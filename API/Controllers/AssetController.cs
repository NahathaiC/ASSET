using API.Data;
using API.DTOs;
using API.DTOs.AssetDtos;
using API.DTOs.StockDtos;
using API.DTOs.UserDtos;
using API.Entities;
using API.Entities.AssetAggregate;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]

    public class AssetController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;
        private readonly UserManager<User> _userManager;
        public AssetController(StoreContext context, IMapper mapper, ImageService imageService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _imageService = imageService;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAssetDto>>> GetAssets()
        {
            var assets = await _context.Assets
                .Include(a => a.Owner)
                .Include(a => a.Stock)
                .ToListAsync();

            var assetDtos = _mapper.Map<List<GetAssetDto>>(assets);

            return assetDtos;
        }

        [HttpGet("Asset", Name = "GetAsset")]
        public async Task<ActionResult<GetAssetDto>> GetAssetById(string id)
        {
            // id = Uri.UnescapeDataString(id); // Decode the URL-encoded ID

            var asset = await _context.Assets
                .Include(a => a.Owner)
                .Include(a => a.Stock)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asset == null)
            {
                return NotFound();
            }

            var assetDto = _mapper.Map<GetAssetDto>(asset);

            return assetDto;
        }

        // [Authorize(Roles = "Admin, Asset")]
        // [HttpPost]
        // public async Task<ActionResult<Asset>> CreateAsset(CreateAssetDto assetDto)
        // {
        //     var asset = _mapper.Map<Asset>(assetDto);

        //     var stock = await _context.Stocks.FindAsync(assetDto.Stock.Id);
        //     if (stock == null)
        //     {
        //         return BadRequest(new ProblemDetails { Title = "Invalid stock." });
        //     }

        //     asset.Stock = stock;

        //     var owner = await _context.Owners.FindAsync(assetDto.Owner.Id);
        //     if (owner == null)
        //     {
        //         return BadRequest(new ProblemDetails { Title = "Invalid owner." });
        //     }

        //     asset.Owner = owner; // Assign the fetched owner to the asset

        //     var personInCharge =  await _userManager.FindByIdAsync(asset.PersonInCharge.Id.ToString());
        //     if (personInCharge == null)
        //     {
        //         return BadRequest("Invalid PersonInCharge");
        //     }

        //     asset.PersonInChargeId = personInCharge.Id;


        //     _context.Assets.Add(asset);

        //     var result = await _context.SaveChangesAsync() > 0;

        //     if (result)
        //     {
        //         return CreatedAtRoute("GetAsset", new { Id = asset.Id }, asset);
        //     }

        //     return BadRequest(new ProblemDetails { Title = "Problem creating a new asset." });
        // }
        public class CreateAssetRequest
        {
            public CreateAssetDto AssetDto { get; set; }
            public PICDto PersonInChargeDto { get; set; }
            public StockDto StockDto { get; set; }
            public OwnerDto OwnerDto { get; set; }
        }

        [Authorize(Roles = "Admin, Asset")]
        [HttpPost]
        [ProducesResponseType(typeof(Asset), 201)]
        public async Task<ActionResult<Asset>> CreateAsset([FromBody] CreateAssetRequest request)
        {
            var asset = _mapper.Map<Asset>(request.AssetDto);

            // Validate the existence of the user (PersonInCharge)
            var personInCharge = await _userManager.FindByIdAsync(request.PersonInChargeDto.Id.ToString());
            if (personInCharge == null)
            {
                return BadRequest("Invalid PersonInCharge");
            }

            // Validate the existence of the stock
            var stock = await _context.Stocks.FindAsync(request.StockDto.Id);
            if (stock == null)
            {
                return BadRequest(new ProblemDetails { Title = "Invalid stock." });
            }

            // Validate the existence of the owner
            var owner = await _context.Owners.FindAsync(request.OwnerDto.Id);
            if (owner == null)
            {
                return BadRequest(new ProblemDetails { Title = "Invalid owner." });
            }

            asset.Stock = stock;
            asset.Owner = owner;
            asset.PersonInChargeId = personInCharge.Id;

            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();

            var createdAsset = _mapper.Map<GetAssetDto>(asset);
            createdAsset.Owner = _mapper.Map<OwnerDto>(owner);
            createdAsset.Stock = _mapper.Map<StockDto>(stock);
            createdAsset.PersonInCharge = _mapper.Map<GetPICDto>(personInCharge);

            return CreatedAtAction(nameof(GetAssetById), new { id = asset.Id }, createdAsset);
        }



        [HttpPut] // Normal, GoodCondition, Broken, UnderRepair
        [Authorize(Roles = "Admin, Asset")]
        public async Task<ActionResult<Asset>> UpdateAssetStatus(string id, AssetStatus status)
        {
            // Check if the provided status value is valid
            if (!Enum.IsDefined(typeof(AssetStatus), status))
            {
                var availableStatusValues = Enum.GetValues(typeof(AssetStatus));
                return BadRequest($"Invalid status value. Available status values: {string.Join(", ", availableStatusValues)}");
            }

            // Retrieve the asset by id from the database
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            // Update the AssetStatus based on the provided status value
            asset.AssetStatus = status;

            // Save the changes to the database
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                // Retrieve the updated asset from the database
                var updatedAsset = await _context.Assets.FindAsync(id);
                return Ok(updatedAsset);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update asset status"); // Return 500 Internal Server Error if the update fails
            }
        }

        [Authorize(Roles = "Admin, Asset")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAsset(string id)
        {
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks.FindAsync(asset.StockId);

            if (stock == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to find the associated stock");
            }

            stock.Total -= 1;

            _context.Assets.Remove(asset);
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting Asset" });
        }

        [Authorize(Roles = "Admin, Asset")]
        [HttpPut("EditAsset")]
        public async Task<ActionResult> EditAsset(UpdateAssetDto assetDto)
        {
            var asset = await _context.Assets.FirstOrDefaultAsync(a => a.Id == assetDto.Id);

            if (asset == null)
            {
                return NotFound();
            }

            _mapper.Map(assetDto, asset); // Map properties from assetDto to asset

            if (asset.StockId != assetDto.Stock.Id)
            {
                var newStock = await _context.Stocks.FindAsync(assetDto.Stock.Id);

                if (newStock == null)
                {
                    return BadRequest(new ProblemDetails
                    {
                        Title = "Invalid stock ID",
                        Detail = "The specified stock ID does not exist."
                    });
                }

                var oldStockId = asset.StockId;
                asset.StockId = newStock.Id;

                await UpdateStockTotal(oldStockId, decrement: true);
                await UpdateStockTotal(newStock.Id, decrement: false);
            }

            var result = await _context.SaveChangesAsync() > 0;

            if (result)
            {
                return NoContent();
            }

            return BadRequest(new ProblemDetails { Title = "Problem Edit Asset" });
        }

        private async Task UpdateStockTotal(int stockId, bool decrement)
        {
            var stock = await _context.Stocks.FindAsync(stockId);

            if (stock != null)
            {
                if (decrement)
                {
                    stock.Total--;
                }
                else
                {
                    stock.Total++;
                }
            }
        }

    }
}
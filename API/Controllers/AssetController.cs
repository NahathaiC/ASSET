using API.Data;
using API.DTOs.AssetDtos;
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
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            var assets = await _context.Assets
                .ToListAsync();

            return assets;
        }

        [HttpGet("{id}", Name = "GetAsset")]
        public async Task<ActionResult<Asset>> GetAsset(string id)
        {
            var asset = await _context.Assets
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asset == null)
            {
                return NotFound();
            }

            return asset;
        }

        [HttpPost]
        public async Task<ActionResult<Asset>> CreateAsset(CreateAssetDto assetDto)
        {
            var asset = _mapper.Map<Asset>(assetDto);

            // Fetch the stock based on the provided StockId
            var stock = await _context.Stocks.FindAsync(assetDto.Stock.Id);
            if (stock == null)
            {
                return BadRequest(new ProblemDetails { Title = "Invalid stock." });
            }

            asset.Stock = stock; // Assign the fetched stock to the asset

            // Fetch the owner based on the provided OwnerId
            var owner = await _context.Owners.FindAsync(assetDto.Owner.Id);
            if (owner == null)
            {
                return BadRequest(new ProblemDetails { Title = "Invalid owner." });
            }

            asset.Owner = owner; // Assign the fetched owner to the asset

            _context.Assets.Add(asset);

            var result = await _context.SaveChangesAsync() > 0;

            if (result)
            {
                return CreatedAtRoute("GetAsset", new { Id = asset.Id }, asset);
            }

            return BadRequest(new ProblemDetails { Title = "Problem creating a new asset." });
        }


        // [HttpPost]
        // public async Task<ActionResult<Asset>> CreateAsset(CreateAssetDto assetDto)
        // {
        //     var asset = _mapper.Map<Asset>(assetDto);

        //     _context.Assets.Add(asset);

        //     var result = await _context.SaveChangesAsync() > 0;

        //     if (result)
        //     {
        //         return CreatedAtRoute("GetAsset", new { Id = asset.Id }, asset);
        //     }

        //     return BadRequest(new ProblemDetails { Title = "Problem creating a new Asset"});
        // }
    }
}
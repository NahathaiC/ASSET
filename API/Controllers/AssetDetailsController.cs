using API.Data;
using API.DTOs.AssetDtos;
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

    public class AssetDetailsController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;
        private readonly UserManager<User> _userManager;
        public AssetDetailsController(StoreContext context, IMapper mapper, ImageService imageService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _imageService = imageService;
            _mapper = mapper;
            _context = context;
        }

        public class GetAssetDetailsRequest
        {
            public GetAssetDetailsDto AssetDetailsDto { get; set; }
            public GetPICDto PersonInChargeDto { get; set; }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAssetDetailsRequest>>> GetAssetDetails()
        {
            var assetDetails = await _context.AssetDetails
                .Include(ad => ad.PersonInCharge)
                .ToListAsync();

            var assetDetailsDto = _mapper.Map<List<GetAssetDetailsRequest>>(assetDetails);

            return Ok(assetDetailsDto);
        }



        // Create a wrapper class for the request body
        public class CreateAssetDetailsRequest
        {
            public CreateAssetDetailsDto AssetDetailsDto { get; set; }
            public PICDto PersonInChargeDto { get; set; }
        }

        [Authorize(Roles = "Asset")]
        [HttpPost]
        [ProducesResponseType(typeof(AssetDetails), 201)]
        public async Task<ActionResult<AssetDetails>> CreateAssetDetails([FromBody] CreateAssetDetailsRequest request)
        {
            var assetDetails = _mapper.Map<AssetDetails>(request.AssetDetailsDto);

            // Validate the existence of the user
            var personInCharge = await _userManager.FindByIdAsync(request.PersonInChargeDto.Id.ToString());
            if (personInCharge == null)
            {
                return BadRequest("Invalid PersonInCharge");
            }

            assetDetails.PersonInChargeId = personInCharge.Id;

            _context.AssetDetails.Add(assetDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAssetDetails), new { id = assetDetails.Id }, assetDetails);
        }

        [Authorize(Roles = "Asset")]
        [HttpPost("AddAssetPicture")]
        public async Task<ActionResult> AddAssetPic([FromForm] AddAssetPicDto addAssetPicDto)
        {
            var assetDetails = await _context.AssetDetails.FindAsync(addAssetPicDto.Id);

            if (assetDetails == null)
                return NotFound();

            if (addAssetPicDto.AssetPic != null)
            {
                var imageResult = await _imageService.AddImageAsync(addAssetPicDto.AssetPic);

                if (imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

                assetDetails.AssetPic = imageResult.SecureUrl.ToString();
                assetDetails.PublicId = imageResult.PublicId;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("assets/active")]
        public async Task<ActionResult<IEnumerable<AssetDetails>>> GetActiveAssetDetails()
        {
            var activeAssetDetails = await _context.GetActiveAssetDetails();

            return Ok(activeAssetDetails);
        }

        [HttpGet("assets/resign")]
        public async Task<ActionResult<IEnumerable<AssetDetails>>> GetResignAssetDetails()
        {
            var resignAssetDetails = await _context.GetResignAssetDetails();

            return Ok(resignAssetDetails);
        }


        [Authorize(Roles = "Asset")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAssetDetails(string id)
        {
            var assetDetails = await _context.AssetDetails.FindAsync(id);

            if (assetDetails == null)
            {
                return NotFound();
            }

            _context.AssetDetails.Remove(assetDetails);
            var result = await _context.SaveChangesAsync() > 0;
            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting AssetDetails" });
        }

        // [Authorize(Roles = "Asset")]
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteAssetDetailswithAsset(string id)
        // {
        //     var assetDetails = await _context.AssetDetails.FindAsync(id);

        //     if (assetDetails == null)
        //     {
        //         return NotFound();
        //     }

        //     var asset = await _context.Assets.FindAsync(assetDetails.AssetId);

        //     if (asset == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.AssetDetails.Remove(assetDetails);
        //     _context.Assets.Remove(asset);

        //     var result = await _context.SaveChangesAsync() > 0;

        //     if (result)
        //     {
        //         return Ok();
        //     }

        //     return BadRequest(new ProblemDetails { Title = "Problem deleting Asset and AssetDetails" });
        // }


    }
}
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetDetails>>> GetAssetDetails()
        {
            var assetDetails = await _context.AssetDetails
                .Include(ad => ad.PersonInCharge) // Include the related PersonInCharge entity
                .ToListAsync();

            return Ok(assetDetails);
        }


        // Create a wrapper class for the request body
        public class CreateAssetDetailsRequest
        {
            public CreateAssetDetailsDto AssetDetailsDto { get; set; }
            public PICDto PersonInChargeDto { get; set; }
        }

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



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAssetDetails(string id)
        {
            var assetDetails = await _context.AssetDetails.FindAsync(id);

            if (assetDetails == null)
            {
                return NotFound();
            }

            _context.AssetDetails.Remove(assetDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
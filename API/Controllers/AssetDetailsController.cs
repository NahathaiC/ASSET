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

        [HttpGet("AssetDetails", Name = "GetAssetDetail")]
        public async Task<ActionResult<GetAssetDetailsRequest>> GetAssetDetailsById(string id)
        {
            // id = Uri.UnescapeDataString(id);

            var assetDetails = await _context.AssetDetails
                .Include(ad => ad.PersonInCharge)
                .FirstOrDefaultAsync(a => a.Id == id);
            
            if (assetDetails == null)
            {
                return NotFound();
            }

            var assetDetailsDto = _mapper.Map<GetAssetDetailsRequest>(assetDetails);

            return assetDetailsDto;
        }

        // Create a wrapper class for the request body
        public class CreateAssetDetailsRequest
        {
            public CreateAssetDetailsDto AssetDetailsDto { get; set; }
            public PICDto PersonInChargeDto { get; set; }
        }

        [Authorize(Roles = "Admin, Asset")]
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

            var createdAssetDetails = _mapper.Map<GetAssetDetailsRequest>(assetDetails);
            createdAssetDetails.PersonInChargeDto = _mapper.Map<GetPICDto>(personInCharge);

            return CreatedAtAction(nameof(GetAssetDetails), new { id = assetDetails.Id }, createdAssetDetails);
        }

        [Authorize(Roles = "Admin, Asset")]
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
        public async Task<ActionResult<IEnumerable<GetAssetDetailsRequest>>> GetActiveAssetDetails()
        {
            var activeAssetDetails = await _context.GetActiveAssetDetails();

            var activeAssetDetailsDto = _mapper.Map<List<GetAssetDetailsRequest>>(activeAssetDetails);

            return Ok(activeAssetDetailsDto);
        }

        [HttpGet("assets/resign")]
        public async Task<ActionResult<IEnumerable<GetAssetDetailsRequest>>> GetResignAssetDetails()
        {
            var resignAssetDetails = await _context.GetResignAssetDetails();

            var resignAssetDetailsDto = _mapper.Map<List<GetAssetDetailsRequest>>(resignAssetDetails);

            return Ok(resignAssetDetailsDto);
        }

        [Authorize(Roles = "Admin, Asset")]
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

        public class EditAssetDetailsRequest
        {
            public UpdateAssetDetailsDto AssetDetailsDto { get; set; }
            public PICDto PersonInChargeDto { get; set; }
        }

        [Authorize(Roles = "Admin, Asset")]
        [HttpPut("EditAssetDetails")]
        public async Task<ActionResult<GetAssetDetailsRequest>> EditAssetDetails([FromBody] EditAssetDetailsRequest request)
        {
            var assetDetails = await _context.AssetDetails.FindAsync(request.AssetDetailsDto.Id);

            if (assetDetails == null)
            {
                return NotFound();
            }

            // Update asset details properties
            _mapper.Map(request.AssetDetailsDto, assetDetails);

            // Validate the existence of the user
            var personInCharge = await _userManager.FindByIdAsync(request.PersonInChargeDto.Id.ToString());
            if (personInCharge == null)
            {
                return BadRequest("Invalid PersonInCharge");
            }

            assetDetails.PersonInChargeId = personInCharge.Id;

            await _context.SaveChangesAsync();

            var updatedAssetDetails = _mapper.Map<GetAssetDetailsRequest>(assetDetails);
            updatedAssetDetails.PersonInChargeDto = _mapper.Map<GetPICDto>(personInCharge);

            return Ok(updatedAssetDetails);
        }

    }
}
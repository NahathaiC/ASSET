using API.Data;
using API.DTOs.TaxDtos;
using API.Entities;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class TaxController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;
        private readonly UserManager<User> _userManager;
        public TaxController(StoreContext context, IMapper mapper, ImageService imageService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _imageService = imageService;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxInvoice>>> GetTaxInvoices()
        {
            var taxInvoices = await _context.TaxInvoices
                .Include(t => t.TaxItems) // Include the TaxItems collection
                .ToListAsync();

            return taxInvoices;
        }


        [HttpGet("{id}", Name = "GetTaxInvoice")]
        public async Task<ActionResult<TaxInvoice>> GetTaxInvoice(int id)
        {
            var taxInvoice = await _context.TaxInvoices
                .Include(t => t.TaxItems) // Include the TaxItems collection
                .SingleOrDefaultAsync(t => t.Id == id);

            if (taxInvoice == null)
            {
                return NotFound();
            }

            return taxInvoice;
        }

        [Authorize(Roles = "Purchasing")]
        [HttpPost("CreateTaxInvoice")] // Unique route for CreateTaxInvoice action
        public async Task<ActionResult> CreateTaxInvoice([FromBody] CreateTaxDto createTaxDto)
        {
            var taxInvoice = _mapper.Map<TaxInvoice>(createTaxDto);

            // Create the list of TaxItem entities
            var taxItems = createTaxDto.TaxItems.Select(taxItemDto => new TaxItem
            {
                ProdDesc = taxItemDto.ProdDesc,
                Id = taxItemDto.Id
            }).ToList();

            taxInvoice.TaxItems = taxItems;

            _context.TaxInvoices.Add(taxInvoice);

            var result = await _context.SaveChangesAsync() > 0;

            if (result)
            {
                await _context.Entry(taxInvoice).ReloadAsync();
                return CreatedAtRoute("GetTaxInvoice", new { Id = taxInvoice.Id }, taxInvoice);
            }

            return BadRequest(new ProblemDetails { Title = "Problem creating a new Tax Invoice" });
        }

        [Authorize(Roles = "Purchasing")]
        [HttpPost("AddTaxInvoicePicture")] // Unique route for AddTaxPic action
        public async Task<ActionResult> AddTaxPic([FromForm] AddTaxPicDto addTaxPicDto)
        {
            var taxInvoice = await _context.TaxInvoices.FindAsync(addTaxPicDto.Id);

            if (taxInvoice == null)
                return NotFound();

            _mapper.Map(addTaxPicDto, taxInvoice);

            if (addTaxPicDto.TaxPics != null)
            {
                var imageResult = await _imageService.AddImageAsync(addTaxPicDto.TaxPics);

                if (imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

                taxInvoice.TaxPics = imageResult.SecureUrl.ToString();
                taxInvoice.PublicId = imageResult.PublicId;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Purchasing")]
        [HttpPut]
        public async Task<ActionResult> UpdateTaxInvoice(UpdateTaxDto taxInvoiceDto)
        {
            var taxInvoice = await _context.TaxInvoices.FindAsync(taxInvoiceDto.Id);

            if (taxInvoice == null) return NotFound();

            _mapper.Map(taxInvoiceDto, taxInvoice);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return NoContent();

            return BadRequest(new ProblemDetails { Title = "Problem updating new Tax Invoice" });
        }

        // [Authorize(Roles = "Purchasing")]
        [HttpDelete]
        public async Task<ActionResult> DeleteTaxInvoice(int id)
        {
            var taxInvoice = await _context.TaxInvoices.FindAsync(id);

            if (taxInvoice == null) return NotFound();

            _context.TaxInvoices.Remove(taxInvoice);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting Tax Invoice" });
        }


    }
}
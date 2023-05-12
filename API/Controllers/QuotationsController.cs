using API.Data;
using API.DTOs.QuotDtos;
using API.Entities;
using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]

    [ApiController]
    [Route("api/[controller]")]
    public class QuotationsController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public QuotationsController(StoreContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("{id}", Name ="GetQuot")]
        public async Task<ActionResult<GetQuotDto>> GetQuot(int id)
        {
            return await _context.Quotations
                // .Include( q => q.Quotation)
                .ProjectQuotToQuotDto()
                .Where( x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Quotation>> CreateQuot(QuotDto quotDto)
        {
            var quotation = _mapper.Map<Quotation>(quotDto);

             _context.Quotations.Add(quotation);

             var result = await _context.SaveChangesAsync() > 0;
             
             if (result) return CreatedAtRoute("GetQuot", new {Id = quotation.Id}, quotation);
             
             return BadRequest(new ProblemDetails { Title = "Problem creating new Quotation"});
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> UpdatePR (UpdateQuotationDto QuotDto)
        {
            var quotation = await _context.Quotations.FindAsync(QuotDto.Id);

            if (quotation == null) return NotFound();

            _mapper.Map(QuotDto, quotation);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return NoContent();

            return BadRequest (new ProblemDetails{Title = "Problen updating Quotation"});
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult> DeleteQuot(int id)
        {
            var quotation = await _context.Quotations.FindAsync(id);

            if (quotation == null) return NotFound();

            _context.Quotations.Remove(quotation);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails{Title = "Problem deleting Quotation"});
        }

    }
}
using API.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using API.DTOs.PODtos;
using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using Microsoft.EntityFrameworkCore;
using API.Entities;
using API.Entities.PRAggregate;

namespace API.Controllers
{
    [Authorize]
    public class POController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        public POController(StoreContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetPODto>>> GetPOs()
        {
            return await _context.PurchaseOrders
            .ProjectPOToPRODto()
            .ToListAsync();
        }

        [HttpGet("{id}", Name = "GetPO")]
        public async Task<ActionResult<GetPODto>> GetPO(int id)
        {
            return await _context.PurchaseOrders
                .ProjectPOToPRODto()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        [Authorize(Roles = "Admin, Purchasing")]
        [HttpPost]
        public async Task<ActionResult<PurchaseOrder>> CreatePO(PODto pODto)
        {
            string userName = User.Identity.Name;

            var purchaseOrder = _mapper.Map<PurchaseOrder>(pODto);
            purchaseOrder.Creator = userName;

            _context.PurchaseOrders.Add(purchaseOrder);

            var result = await _context.SaveChangesAsync() > 0;

            if (result)
            {
                return CreatedAtRoute("GetPO", new { Id = purchaseOrder.Id }, purchaseOrder);
            }
            else
            {
                return BadRequest(new ProblemDetails { Title = "Problem Creating New PO" });
            }
        }

        [Authorize(Roles = "Admin, Purchasing")]
        [HttpPost("{id}/quotation/{quotationId}")]
        public async Task<ActionResult> AddQuotation(int id, int quotationId)
        {
            string userName = User.Identity.Name;

            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);

            if (purchaseOrder == null)
                return NotFound(); // Return 404 Not Found if the purchaseOrder is not found

            if (purchaseOrder.Creator != userName && !User.IsInRole("Admin"))
                return Forbid(); // Return 403 Forbidden if the authenticated user is not the creator or doesn't have the "Admin" role

            var quotation = await _context.Quotations.FindAsync(quotationId);

            if (quotation == null)
                return NotFound(); // Return 404 Not Found if the quotation is not found

            // Associate the quotation with the purchaseOrder
            purchaseOrder.Quotation = quotation;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        [Route("PurchaseOrder/{id}/status")]
        [Authorize(Roles = "Approver")]
        public async Task<ActionResult<PurchaseOrder>> UpdateStatus(int id, string status)
        {
            string userName = User.Identity.Name;

            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            if (status == "Approved" || status == "approved")
            {
                purchaseOrder.ApproverName1 = userName;
                purchaseOrder.Status = Status.Approved;
            }
            else if (status == "Disapproved" || status == "disapproved")
            {
                purchaseOrder.Status = Status.Disapproved;
            }
            else if (status == "Cancel" || status == "cancel")
            {
                purchaseOrder.Status = Status.Cancel;
            }
            else
            {
                return BadRequest("Invalid status value"); // Return 400 Bad Request if the status value is invalid
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                var updarePurchaseOrder = await _context.PurchaseOrders.FindAsync(id);
                return Ok(updarePurchaseOrder);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to Update Status");
            }
        }

        [Authorize(Roles = "Admin, Purchasing")]
        [HttpPut("EditPurchaseOrders")]
        public async Task<ActionResult> UpdatePO(UpdatePODto poDto, int id)
        {
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (purchaseOrder.Quotation != null)
            {
                if (poDto.Quotation != null && poDto.Quotation.Id != purchaseOrder.Quotation.Id)
                {
                    return BadRequest("The PurchaseOrder already has a Quotation assigned and cannot be modified.");
                }
            }
            else if (poDto.Quotation != null)
            {
                var existingQuotation = await _context.Quotations.FindAsync(poDto.Quotation.Id);

                if (existingQuotation == null)
                {
                    return BadRequest("Invalid Quotation. Please provide an existing Quotation.");
                }

                // Detach the existing Quotation from the context if it's not null
                if (purchaseOrder.Quotation != null)
                {
                    _context.Entry(purchaseOrder.Quotation).State = EntityState.Detached;
                }

                // Assign the new Quotation to the purchaseOrder
                purchaseOrder.Quotation = existingQuotation;
            }

            _mapper.Map(poDto, purchaseOrder);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Problem editing PO",
                    Detail = ex.Message
                });
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin, Purchasing")]
        [HttpDelete]
        public async Task<ActionResult> DeletePO(int id)
        {
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);

            if (purchaseOrder == null) return NotFound();

            _context.PurchaseOrders.Remove(purchaseOrder);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem Deleting PO" });
        }

    }
}
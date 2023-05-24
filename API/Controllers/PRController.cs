using API.Data;
using API.DTOs.PRDtos;
using API.Entities.PRAggregate;
using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class PRController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        public PRController(StoreContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<GetPRDto>>> GetPRs()
        {
            return await _context.PurchaseRequisitions

                .ProjectPRToPRDto()
                .ToListAsync();
        }

        [HttpGet("{id}", Name = "GetPR")]
        public async Task<ActionResult<GetPRDto>> GetPR(int id)
        {
            return await _context.PurchaseRequisitions

                .ProjectPRToPRDto()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseRequisition>> CreatePR(PRDto prDto)
        {
            // Get the UserName of the user creating the PurchaseRequisition
            string userName = User.Identity.Name;

            var purchaseRequisition = _mapper.Map<PurchaseRequisition>(prDto);
            purchaseRequisition.RequestUser = userName; // Set the RequestUser property to the UserName

            _context.PurchaseRequisitions.Add(purchaseRequisition);

            var result = await _context.SaveChangesAsync() > 0;

            if (result)
            {
                return CreatedAtRoute("GetPR", new { Id = purchaseRequisition.Id }, purchaseRequisition);
            }
            else
            {
                return BadRequest(new ProblemDetails { Title = "Problem creating new PR" });
            }
        }

        [HttpPost("AddQuotationToPurchaseRequisition")]
        public async Task<ActionResult> AddQuotation(int id, int quotationId)
        {
            string userName = User.Identity.Name;

            var purchaseRequisition = await _context.PurchaseRequisitions.FindAsync(id);

            if (purchaseRequisition == null)
                return NotFound(); // Return 404 Not Found if the purchaseRequisition is not found

            if (purchaseRequisition.RequestUser != userName && !User.IsInRole("Admin"))
                return Forbid(); // Return 403 Forbidden if the authenticated user is not the request user and doesn't have the "Admin" role

            var quotation = await _context.Quotations.FindAsync(quotationId);

            if (quotation == null)
                return NotFound(); // Return 404 Not Found if the quotation is not found

            // Associate the quotation with the purchaseRequisition
            purchaseRequisition.Quotation = quotation;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Approver")]
        [HttpPut]
        [Route("UpdateStatusPurchaseRequisition")]
        public async Task<ActionResult<PurchaseRequisition>> UpdateStatus(int id, string status)
        {
            string userName = User.Identity.Name;
            // Retrieve the purchaseRequisition by id from the database
            var purchaseRequisition = await _context.PurchaseRequisitions.FindAsync(id);

            if (purchaseRequisition == null)
            {
                return NotFound(); // Return 404 Not Found if the purchaseRequisition is not found
            }

            // Update the Status based on the provided status value
            if (status == "Approved" || status == "approved")
            {
                // Check if the purchaseRequisition has already received two approvals
                if (purchaseRequisition.ApprovalsReceived >= 2)
                {
                    return BadRequest("The purchase requisition has already received the maximum number of approvals."); // Return 400 Bad Request
                }

                // Increment the number of approvals received
                purchaseRequisition.ApprovalsReceived++;

                // Check if the required number of approvals has been received
                if (purchaseRequisition.ApprovalsReceived == 1)
                {
                    purchaseRequisition.ApproverName1 = userName;
                }
                else if (purchaseRequisition.ApprovalsReceived == 2)
                {
                    purchaseRequisition.ApproverName2 = userName;
                    // Set the Status to "Approved"
                    purchaseRequisition.Status = Status.Approved;
                }
            }
            else if (status == "Disapproved" || status == "disapproved")
            {
                purchaseRequisition.Status = Status.Disapproved;
            }
            else if (status == "Cancel" || status == "cancel")
            {
                purchaseRequisition.Status = Status.Cancel;
            }
            else
            {
                return BadRequest("Invalid status value"); // Return 400 Bad Request if the status value is invalid
            }

            // Save the changes to the database
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                // Retrieve the updated purchaseRequisition from the database
                var updatedPurchaseRequisition = await _context.PurchaseRequisitions.FindAsync(id);
                return Ok(updatedPurchaseRequisition); // Return the updated purchaseRequisition
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update Status"); // Return 500 Internal Server Error if the update fails
            }
        }

        [HttpPut]
        [Route("CanclePurchaseRequisitionByCreator")]
        [Authorize]
        public async Task<ActionResult<PurchaseRequisition>> UpdateStatusByEmp(int id)
        {
            string userName = User.Identity.Name;

            var purchaseRequisition = await _context.PurchaseRequisitions.FindAsync(id);

            if (purchaseRequisition == null)
            {
                return NotFound();
            }

            // Check if the authenticated user is authorized to update the status
            if (purchaseRequisition.RequestUser != userName)
            {
                return Forbid(); // Return 403 Forbidden if the authenticated user is not the request user
            }

            purchaseRequisition.Status = Status.Cancel;

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Ok(purchaseRequisition); // Return the updated purchase requisition
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update Status");
            }
        }

        [Authorize(Roles = "Admin, RequestUser")]
        [HttpPut("EditPurchaseRequisition")]
        public async Task<ActionResult> UpdatePR(UpdatePRDto prDto, int id)
        {
            var purchaseRequisition = await _context.PurchaseRequisitions.FindAsync(id);

            if (purchaseRequisition == null)
            {
                return NotFound();
            }

            // Check if the user is the RequestUser or an Admin
            string userName = User.Identity.Name;
            bool isRequestUser = userName == purchaseRequisition.RequestUser;
            bool isAdmin = User.IsInRole("Admin");

            if (!isRequestUser && !isAdmin)
            {
                return Forbid();
            }

            if (purchaseRequisition.Quotation != null)
            {
                if (prDto.Quotation != null && prDto.Quotation.Id != purchaseRequisition.Quotation.Id)
                {
                    return BadRequest("The PurchaseRequisition already has a Quotation assigned and cannot be modified.");
                }
            }
            else if (prDto.Quotation != null)
            {
                var existingQuotation = await _context.Quotations.FindAsync(prDto.Quotation.Id);

                if (existingQuotation == null)
                {
                    return BadRequest("Invalid Quotation. Please provide an existing Quotation.");
                }

                if (purchaseRequisition.Quotation != null)
                {
                    _context.Entry(purchaseRequisition.Quotation).State = EntityState.Detached;
                }

                purchaseRequisition.Quotation = existingQuotation;
            }

            _mapper.Map(prDto, purchaseRequisition);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Problem editing PR",
                    Detail = ex.Message
                });
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult> DeletePR(int id)
        {
            var purchaseRequisition = await _context.PurchaseRequisitions.FindAsync(id);

            if (purchaseRequisition == null) return NotFound();

            _context.PurchaseRequisitions.Remove(purchaseRequisition);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting PR" });
        }

    }
}
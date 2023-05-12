using API.DTOs.PRDtos;
using API.Entities.PRAggregate;

namespace API.Extensions
{
    public static class PRExtensions
    {
        public static IQueryable<GetPRDto> ProjectPRToPRDto(this IQueryable<PurchaseRequisition> query)
        {
            return query
                .Select(purchaseRequisition => new GetPRDto
                {
                    Id = purchaseRequisition.Id,
                    RequestUser = purchaseRequisition.RequestUser,
                    FixHistory = purchaseRequisition.FixHistory,
                    CreateDate = purchaseRequisition.CreateDate,
                    Title = purchaseRequisition.Title,
                    Department = purchaseRequisition.Department,
                    Section = purchaseRequisition.Section,
                    UseDate = purchaseRequisition.UseDate,
                    ProdDesc = purchaseRequisition.ProdDesc,
                    Model = purchaseRequisition.Model,
                    Quantity = purchaseRequisition.Quantity,
                    UnitPrice = purchaseRequisition.UnitPrice,
                    Remark = purchaseRequisition.Remark,
                    Quotation = purchaseRequisition.Quotation,
                    Status = purchaseRequisition.Status.ToString(),
                    ApproverName1 = purchaseRequisition.ApproverName1,
                    ApproverName2 = purchaseRequisition.ApproverName2,
                });
        }
    }
}
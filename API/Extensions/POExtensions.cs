using API.DTOs.PODtos;
using API.Entities;

namespace API.Extensions
{
    public static class POExtensions
    {
        public static IQueryable<GetPODto> ProjectPOToPRODto(this IQueryable<PurchaseOrder> query)
        {
            return query
                .Select(purchaseOrder => new GetPODto
                {
                    Id = purchaseOrder.Id,
                    Creator = purchaseOrder.Creator,
                    Bought_date = purchaseOrder.Bought_date,
                    ProdDesc = purchaseOrder.ProdDesc,
                    Model = purchaseOrder.Model,
                    Quantity = purchaseOrder.Quantity,
                    UnitPrice = purchaseOrder.UnitPrice,
                    TotalPrice = purchaseOrder.TotalPrice,
                    ApproverName1 = purchaseOrder.ApproverName1,
                    Status = purchaseOrder.Status.ToString(),
                    Quotation = purchaseOrder.Quotation
                    
                });
        }
    }
}
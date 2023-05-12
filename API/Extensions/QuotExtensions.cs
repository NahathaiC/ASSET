using API.DTOs.QuotDtos;
using API.Entities;

namespace API.Extensions
{
    public static class QuotExtensions
    {
        public static IQueryable<GetQuotDto> ProjectQuotToQuotDto(this IQueryable<Quotation> query)
        {
            return query
                .Select(quotation => new GetQuotDto
                {
                    Id = quotation.Id,
                    Supplier = quotation.Supplier,
                    TotalPrice = quotation.TotalPrice,
                    CreateDate = quotation.CreateDate,
                    Remark = quotation.Remark

                });
        }
    }
}
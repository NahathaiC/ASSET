using System.ComponentModel.DataAnnotations;

namespace API.DTOs.QuotDtos
{
    public class UpdateQuotationDto
    {
        public int Id { get; set; }
        public string Supplier { get; set; }
        [Range(1,double.PositiveInfinity)]
        public decimal TotalPrice { get; set; }
        public string Remark { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.QuotDtos
{
    public class UpdateQuotationDto
    {
        public string Supplier { get; set; }
        public decimal TotalPrice { get; set; }
        public string Remark { get; set; }
    }
}
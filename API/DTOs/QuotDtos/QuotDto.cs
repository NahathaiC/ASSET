using System.ComponentModel.DataAnnotations;

namespace API.DTOs.QuotDtos
{
    public class QuotDto
    {
        public string Supplier { get; set; }
        [Required]
        [Range(1,double.PositiveInfinity)]
        public decimal TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public string Remark { get; set; }
    }
}
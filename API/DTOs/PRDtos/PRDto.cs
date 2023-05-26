using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs.PRDtos
{
    public class PRDto
    {
        public string FixHistory { get; set; }
        // public DateTime CreateDate { get; set; } = DateTime.Now;
        [Required]
        public string Title { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        [Required]
        public DateTime UseDate { get; set; }
        [Required]
        public string ProdDesc { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        [Range(1,double.PositiveInfinity)]
        public int Quantity { get; set; }
        [Required]
        [Range(1,double.PositiveInfinity)]
        public decimal UnitPrice { get; set; }
        public string Remark { get; set; }
        // public Quotation Quotation { get; set; }
        public IFormFile PrPicture { get; set; }
    }
}
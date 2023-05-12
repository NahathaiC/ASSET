using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs.PODtos
{
    public class PODto
    {
        [Required]
        public DateTime Bought_date { get; set; }
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
        [Required]
        [Range(1,double.PositiveInfinity)]
        public decimal TotalPrice { get; set; }
        public Quotation Quotation { get; set; }
    }
}
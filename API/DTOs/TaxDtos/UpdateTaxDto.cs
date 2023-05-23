using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs.TaxDtos
{
    public class UpdateTaxDto
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public string Supplier { get; set; }

        [Required]
        public string SuppAdress { get; set; }
        public List<TaxItem> TaxItems { get; set; }

        [Required]
        public string Product_Desc { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Range(1, double.PositiveInfinity)]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Range(0, double.PositiveInfinity)]
        public decimal Discount { get; set; }

        [Range(0, double.PositiveInfinity)]
        public decimal AftDiscount { get; set; }

        [Required]
        [Range(0, double.PositiveInfinity)]
        public decimal Vat { get; set; }

        [Required]
        [Range(1, double.PositiveInfinity)]
        public decimal GrandAmount { get; set; }
    }
}
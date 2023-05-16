using System.ComponentModel.DataAnnotations;

namespace API.DTOs.TaxDtos
{
    public class CreateTaxDto
    {
        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        // [Required]
        // public IFormFile TaxPics { get; set; }

        [Required]
        public string Supplier { get; set; }

        [Required]
        public string SuppAdress { get; set; }

        [Required]
        public List<TaxItemDto> TaxItems { get; set; } = new List<TaxItemDto>();

        [Required]
        public string Product_Desc { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Unit price must be greater than or equal to 0.")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total amount must be greater than or equal to 0.")]
        public decimal TotalAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Discount must be greater than or equal to 0.")]
        public decimal Discount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "After discount amount must be greater than or equal to 0.")]
        public decimal AftDiscount { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "VAT must be greater than or equal to 0.")]
        public decimal Vat { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Grand amount must be greater than 0.")]
        public decimal GrandAmount { get; set; }

        public class TaxItemDto
        {
            [Required]
            public string Id { get; set; }

            [Required]
            public string ProdDesc { get; set; }
        }
    }
}
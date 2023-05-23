using System.ComponentModel.DataAnnotations;

namespace API.DTOs.AssetDtos
{
    public class CreateAssetDetailsDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string AssetId { get; set; }

        // [Required]
        public DateTime ReceivedDate { get; set; } = DateTime.Now;

        // [Required]
        public string Classifier { get; set; }

        // [Required]
        public string SerialNo { get; set; }

        // [Required]
        public string Supplier { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Vat { get; set; }
        public decimal DepreciationRate { get; set; }
        public decimal GrandAmount { get; set; }
        public decimal UsedMonths { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string LocateAt { get; set; }
        public decimal Depreciation { get; set; }
    }
}
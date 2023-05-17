using System.ComponentModel.DataAnnotations.Schema;
using API.Entities;

namespace API.DTOs.AssetDtos
{
    public class CreateAssetDetailsDto
    {
        public int AssetId { get; set; }
        public DateTime ReceivedDate { get; set; } = DateTime.Now;

        [ForeignKey("PersonInChargeId")]
        public int PersonInChargeId { get; set; }
        public User PersonInCharge { get; set; }
        public string AssetPic { get; set; }
        public string Classifier { get; set; }
        public string SerialNo { get; set; }
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
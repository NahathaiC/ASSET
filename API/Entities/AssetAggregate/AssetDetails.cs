using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.AssetAggregate
{
    [Owned]
    public class AssetDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AssetId { get; set; }
        public DateTime ReceivedDate { get; set; } = DateTime.Now;

        [ForeignKey("PersonInChargeId")]  // Specify the foreign key property name
        public int PersonInChargeId { get; set; }  // Foreign key property

        public User PersonInCharge { get; set; }
        public IFormFile AssetPic { get; set; }
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

        public decimal GetDepreciation ()
        {
            return GrandAmount * DepreciationRate * UsedMonths ;
        }
    }
}
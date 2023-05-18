using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.AssetAggregate
{
    [Owned]
    public class AssetDetails
    {
        [Key]
        public string Id { get; set; }
        public string AssetId { get; set; }
        public DateTime ReceivedDate { get; set; } = DateTime.Now;

        [ForeignKey("PersonInChargeId")]  // Specify the foreign key property name
        public int PersonInChargeId { get; set; }  // Foreign key property
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
        public decimal Depreciation => GrandAmount * (DepreciationRate / 100) * (UsedMonths / 12);
        public string PublicId { get; set; }

        internal static IEnumerable<object> Include(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.AssetAggregate
{
    public class Asset
    {
        [Key]
        public string Id { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        public Owner OwnerDesc { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public AssetStatus AssetStatus { get; set; } = AssetStatus.GoodCondition;
        
        [ForeignKey("StockId")]
        public Stock Stock { get; set; }
        public int StockId { get; set; }

    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Entities.AssetAggregate;
using API.DTOs.StockDtos;
using API.DTOs;

namespace API.Entities.AssetAggregate
{
    public class Asset
    {
        [Key]
        public string Id { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        /// <summary>
        /// Id 1 = "ทรัพย์สินของ บริษัท", Id 2 = "ทรัพย์สินของ คู่สัญญา"
        /// </summary>
        [ForeignKey("OwnerId")]
        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public AssetStatus AssetStatus { get; set; } = AssetStatus.GoodCondition;

        [ForeignKey("StockId")]
        public Stock Stock { get; set; }
        public int StockId { get; set; }
    }
}

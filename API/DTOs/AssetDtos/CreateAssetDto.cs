using System.ComponentModel.DataAnnotations.Schema;
using API.Entities;

namespace API.DTOs.AssetDtos
{
    public class CreateAssetDto
    {
        public int No { get; set; }
        public string Name { get; set; }
        public Owner OwnerDesc { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        // public AssetStatus AssetStatus { get; set; }

        [ForeignKey("StockId")]
        public Stock Stock { get; set; }
        public int StockId { get; set; }

    }
}
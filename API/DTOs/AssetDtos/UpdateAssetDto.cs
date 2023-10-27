using System.ComponentModel.DataAnnotations;
using API.DTOs.StockDtos;

namespace API.DTOs.AssetDtos
{
    public class UpdateAssetDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public int No { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
        public string SerialNo { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public OwnerDto Owner { get; set; }

        public StockDto Stock { get; set; }
    }
}
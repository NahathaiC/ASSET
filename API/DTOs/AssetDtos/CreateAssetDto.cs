using API.DTOs.StockDtos;

namespace API.DTOs.AssetDtos
{
    public class CreateAssetDto
    {
        public string Id { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        public OwnerDto Owner { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public StockDto Stock { get; set; }
    }

}
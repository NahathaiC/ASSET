using API.DTOs.StockDtos;
using API.DTOs.UserDtos;

namespace API.DTOs.AssetDtos
{
    public class GetAssetDto
    {
        public string Id { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string SerialNo { get; set; }
        public OwnerDto Owner { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string AssetStatus { get; set; }
        public StockDto Stock { get; set; }
        public GetPICDto PersonInCharge { get; internal set; }
        public string AssetPic { get; set; }

    }
}
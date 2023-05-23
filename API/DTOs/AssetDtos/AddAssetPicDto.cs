using System.ComponentModel.DataAnnotations;

namespace API.DTOs.AssetDtos
{
    public class AddAssetPicDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public IFormFile AssetPic { get; set; }
    }
}
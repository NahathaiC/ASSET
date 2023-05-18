using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
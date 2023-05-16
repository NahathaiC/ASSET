using System.ComponentModel.DataAnnotations;


namespace API.DTOs.TaxDtos
{
    public class AddTaxPicDto
    {
        public int Id { get; set; }
        [Required]
        public IFormFile TaxPics { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.UserDtos
{
    public class RegisterDto : LoginDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Id { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Section { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
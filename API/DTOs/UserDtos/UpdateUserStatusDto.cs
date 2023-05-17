namespace API.DTOs.UserDtos
{
    public class UpdateUserStatusDto
    {
        public string EmailOrId { get; set; }
        public string Status { get; set; }
    }
}

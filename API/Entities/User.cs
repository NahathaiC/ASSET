using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class User : IdentityUser<int>
    {
        [PersonalData]
        public string Position { get; set; }
        [PersonalData]
        public string Department { get; set; }
        [PersonalData]
        public string Section { get; set; }
        [PersonalData]
        public string Phone { get; set; }
        [PersonalData]
        public string Status { get; set; } = "Active";
    }
}
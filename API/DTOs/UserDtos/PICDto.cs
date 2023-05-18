using System.Text.Json.Serialization;

namespace API.DTOs.UserDtos
{
    public class PICDto
    {
        public int Id { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string UserName { get; set; }

        [JsonIgnore]
        public string NormalizedUserName { get; set; }

        [JsonIgnore]
        public string NormalizedEmail { get; set; }

        [JsonIgnore]
        public bool EmailConfirmed { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public string SecurityStamp { get; set; }

        [JsonIgnore]
        public string ConcurrencyStamp { get; set; }

        [JsonIgnore]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public bool PhoneNumberConfirmed { get; set; }

        [JsonIgnore]
        public bool TwoFactorEnabled { get; set; }

        [JsonIgnore]
        public DateTimeOffset? LockoutEnd { get; set; }

        [JsonIgnore]
        public bool LockoutEnabled { get; set; }

        [JsonIgnore]
        public int AccessFailedCount { get; set; }

        [JsonIgnore]
        public string Position { get; set; }

        [JsonIgnore]
        public string Department { get; set; }

        [JsonIgnore]
        public string Section { get; set; }

        [JsonIgnore]
        public string Phone { get; set; }

        [JsonIgnore]
        public string Status { get; set; }
    }
}

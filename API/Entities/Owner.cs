using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        public string OwnerDesc { get; set; }
    }
}
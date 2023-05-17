using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public int Total { get; set; }
    }
}
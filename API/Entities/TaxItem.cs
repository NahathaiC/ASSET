using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Owned]
    public class TaxItem
    {
        [Key]
        public string Id { get; set; }
        public string ProdDesc { get; set; } 
    }
}
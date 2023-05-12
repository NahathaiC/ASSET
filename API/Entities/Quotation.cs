using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Owned]
    public class Quotation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        // public int Id {  get; private set; }
        public string Supplier { get; set; }
        [Range(1,double.PositiveInfinity)]
        public decimal TotalPrice { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Remark { get; set; }
    }
}
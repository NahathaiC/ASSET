using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs.PRDtos
{
    public class UpdatePRDto
    {
        public string FixHistory { get; set; }
        // public DateTime CreateDate { get; set; }
        
        public string Title { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        
        public DateTime UseDate { get; set; }
        
        public string ProdDesc { get; set; }
        
        public string Model { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Remark { get; set; }
        public Quotation Quotation { get; set; }
    }
}
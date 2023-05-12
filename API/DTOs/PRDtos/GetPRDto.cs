using API.Entities;

// This DTO is for HttpGet 
namespace API.DTOs.PRDtos
{
    public class GetPRDto
    {
        public int Id { get; set; }
        public string RequestUser { get; set; }
        public string FixHistory { get; set; }
        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public DateTime UseDate { get; set; }
        public string ProdDesc { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public string ApproverName1 { get; set; }
        public string ApproverName2 { get; set; }
        public Quotation Quotation { get; set; }
    }
}
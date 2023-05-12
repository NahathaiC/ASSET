using API.Entities;

namespace API.DTOs.PODtos
{
    public class GetPODto
    {
        public int Id { get; set; }
        public string Creator { get; set; }
        public DateTime Bought_date { get; set; }
        public string ProdDesc { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string ApproverName1 { get; set; }
        public string Status { get; set; }
        public Quotation Quotation { get; set; }
    }
}
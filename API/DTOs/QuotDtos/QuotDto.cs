namespace API.DTOs.QuotDtos
{
    public class QuotDto
    {
        public string Supplier { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public string Remark { get; set; }
    }
}
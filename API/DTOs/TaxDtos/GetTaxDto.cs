using API.Entities;

namespace API.DTOs.TaxDtos
{
    public class GetTaxDto
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Supplier { get; set; }
        public string SuppAdress { get; set; }
        public List<TaxItem> TaxItems { get; set; }
        public string Product_Desc { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal AftDiscount { get; set; }
        public decimal Vat { get; set; }
        public decimal GrandAmount { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class TaxInvoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        // public string TaxNumber { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string TaxPics { get; set; }
        public string Supplier { get; set; }
        public string SuppAddress { get; set; }
        public List<TaxItem> TaxItems { get; set; } = new List<TaxItem>();
        public string Product_Desc { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal AftDiscount { get; set; }
        public decimal Vat { get; set; }
        public decimal GrandAmount { get; set; }
        public string PublicId { get; set; }
    }
    
}

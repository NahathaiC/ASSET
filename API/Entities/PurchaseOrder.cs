using System.ComponentModel.DataAnnotations.Schema;
using API.Entities.PRAggregate;

namespace API.Entities
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public string Creator { get; set; }
        public DateTime Bought_date { get; set; }
        public string ProdDesc { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        [Column("ApproverName1")]
        public string ApproverName1 { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public Quotation Quotation { get; set; }
    }
}
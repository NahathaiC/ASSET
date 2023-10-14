using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.PRAggregate
{
    public class PurchaseRequisition
    {
        public int Id { get; set; }
        public string RequestUser { get; set; }
        //public string FixHistory { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Title { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public DateTime UseDate { get; set; }
        public string ProdDesc { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Remark { get; set; }
        [Column("ApproverName1")]
        public string ApproverName1 { get; set; }
        [Column("ApproverName2")]
        public string ApproverName2 { get; set; }
        public int ApprovalsReceived { get; set; } = 0;
        public Status Status { get; set; } = Status.Pending;
        public Quotation Quotation { get; set; }
        public string PrPicture { get; set; }
        public string PublicId { get; set; }

    }
}

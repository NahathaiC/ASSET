namespace API.DTOs.AssetDtos
{
    public class GetAssetDetailsDto
    {
        public string Id { get; set; }
        public string AssetId { get; set; }
        public string AssetPic { get; set; }
        // public int TaxInvoiceId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string Classifier { get; set; }
        public string SerialNo { get; set; }
        public string Supplier { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Vat { get; set; }
        public decimal DepreciationRate { get; set; }
        public decimal GrandAmount { get; set; }
        public decimal UsedMonths { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string LocateAt { get; set; }
        public decimal Depreciation { get; set; }
    }
}
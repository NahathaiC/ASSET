namespace API.RequestHelpers
{
    public class AssetParams : PaginationParams
    {
        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
    }
}
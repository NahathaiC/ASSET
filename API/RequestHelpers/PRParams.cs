namespace API.RequestHelpers
{
    public class PRParams : PaginationParams
    {
        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
    }
}
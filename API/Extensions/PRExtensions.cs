using API.Entities.PRAggregate;

namespace API.Extensions
{
    public static class PRExtensions
    {
        public static IQueryable<PurchaseRequisition> Sort(this IQueryable<PurchaseRequisition> query, string orderBy)
        {
            switch (orderBy)
            {
                case "CreatedDate":
                    query = query.OrderBy(p => p.CreateDate);
                    break;
                case "UsingDate":
                    query = query.OrderBy(p => p.UseDate);
                    break;

                default:
                    query = query.OrderBy(p => p.Id);
                    break;
            }

            return query;
        }

        public static IQueryable<PurchaseRequisition> Search(this IQueryable<PurchaseRequisition> query, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return query;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return query.Where(p => p.Department.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<PurchaseRequisition> Filter(this IQueryable<PurchaseRequisition>query, string department, string section )
        {
            var departmentList = new List<string>();
            var sectionList = new List<string>();

            if(!string.IsNullOrEmpty(department))
                departmentList.AddRange(department.ToLower().Split(",").ToList());
            
            if(!string.IsNullOrEmpty(section))
                sectionList.AddRange(section.ToLower().Split(",").ToList());
            
            query = query.Where( p => departmentList.Count == 0 || departmentList.Contains(p.Department.ToLower()));
            query = query.Where( p => sectionList.Count == 0 || sectionList.Contains(p.Section.ToLower()));

            return query;
        }

    }
}

using API.Entities.AssetAggregate;

namespace API.Extensions
{
    public static class AssetExtensions
    {
        public static IQueryable<Asset> Sort(this IQueryable<Asset>query, string orderBy)
        {
            switch (orderBy)
            {
                case "No":
                    query = query.OrderBy(x => x.No);
                    break;
                default:
                    query = query.OrderBy(x => x.Id);
                    break;
            }
            return query;
        }

        public static IQueryable<Asset> Search(this IQueryable<Asset> query, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return query;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(x => x.Name .ToLower().Contains(lowerCaseSearchTerm));
        }
    }
}
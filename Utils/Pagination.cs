using Microsoft.EntityFrameworkCore;

namespace score_management_be.Utils
{
    public class Pagination<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPage => (int)Math.Ceiling((double)TotalCount / PageSize);
    }

    public static class IQueryableExtensions
    {
        public static async Task<Pagination<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 1;

            var totalCount = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new Pagination<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }
    }
}

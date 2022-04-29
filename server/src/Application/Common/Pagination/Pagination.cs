using Microsoft.EntityFrameworkCore;

namespace Application.Common.Pagination
{
    public class Pagination<T>
    {
        public Pagination(IEnumerable<T> items, PaginationResult result)
        {
            Items = items;
            Result = result;
        }
        public IEnumerable<T> Items { get; private set; }
        public PaginationResult Result { get; set; }
        public static async Task<Pagination<T>> CreateAsync(IQueryable<T> source, PaginationParams param)
        {
            var count = await source.CountAsync();
            var pagResult = new PaginationResult(count, param.PageNumber, param.PageSize);
            var items = await source
                .Skip((pagResult.CurrentPage - 1) * pagResult.PageSize)
                .Take(pagResult.PageSize)
                .ToListAsync();


            return new Pagination<T>(items, pagResult);
        }
    }
}
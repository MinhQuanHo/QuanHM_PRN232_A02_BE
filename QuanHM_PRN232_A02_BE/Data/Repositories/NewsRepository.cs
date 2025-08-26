using Microsoft.EntityFrameworkCore;
using QuanHM_PRN232_A02_BE.Data.Interfaces;
using QuanHM_PRN232_A02_BE.Models;

namespace QuanHM_PRN232_A02_BE.Data.Repositories
{
    public class NewsRepository : Repository<NewsArticle>, INewsRepository
    {
        private readonly FUNewsManagementContext _ctx;
        public NewsRepository(FUNewsManagementContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<(IEnumerable<NewsArticle> items, int totalCount)> SearchAsync(
            string? keyword, short? categoryId, short? createdBy,
            int page, int pageSize, string sortBy, string sortOrder)
        {
            var query = _ctx.NewsArticles.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(n => n.NewsTitle!.Contains(keyword) || n.NewsContent!.Contains(keyword));

            if (categoryId.HasValue)
                query = query.Where(n => n.CategoryId == categoryId.Value);

            if (createdBy.HasValue)
                query = query.Where(n => n.CreatedById == createdBy.Value);

            int totalCount = await query.CountAsync();

            // Sorting
            sortBy = sortBy.ToLower();
            sortOrder = sortOrder.ToLower();
            query = sortBy switch
            {
                "title" => sortOrder == "asc" ? query.OrderBy(n => n.NewsTitle) : query.OrderByDescending(n => n.NewsTitle),
                "headline" => sortOrder == "asc" ? query.OrderBy(n => n.Headline) : query.OrderByDescending(n => n.Headline),
                "createddate" or _ => sortOrder == "asc"
                    ? query.OrderBy(n => n.CreatedDate)
                    : query.OrderByDescending(n => n.CreatedDate),
            };

            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, totalCount);
        }
    }
}

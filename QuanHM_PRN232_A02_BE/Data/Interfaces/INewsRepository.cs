using QuanHM_PRN232_A02_BE.Models;

namespace QuanHM_PRN232_A02_BE.Data.Interfaces
{
    public interface INewsRepository : IRepository<NewsArticle>
    {
        Task<(IEnumerable<NewsArticle> items, int totalCount)> SearchAsync(
            string? keyword,
            short? categoryId,
            short? createdBy,
            int page,
            int pageSize,
            string sortBy,
            string sortOrder
        );
    }
}

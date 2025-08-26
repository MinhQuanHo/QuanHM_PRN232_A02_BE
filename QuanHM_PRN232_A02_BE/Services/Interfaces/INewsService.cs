using QuanHM_PRN232_A02_BE.DTOs;

namespace QuanHM_PRN232_A02_BE.Services.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDto>> GetPublicAsync();
        Task<IEnumerable<NewsDto>> GetMyNewsAsync(string email);
        Task<NewsDto?> GetByIdAsync(string id, string email);
        Task AddAsync(NewsDto dto, string email);
        Task<bool> UpdateAsync(string id, NewsDto dto, string email);
        Task<bool> DeleteAsync(string id, string email);

        Task<(IEnumerable<NewsDto> items, int totalCount)> SearchAsync(
            string? keyword, short? categoryId, short? createdBy,
            int page, int pageSize, string sortBy, string sortOrder);
    }
}

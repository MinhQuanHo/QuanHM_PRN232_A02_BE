using QuanHM_PRN232_A02_BE.DTOs;

namespace QuanHM_PRN232_A02_BE.Services.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagDto>> GetAllAsync();
        Task<TagDto?> GetByIdAsync(int id);
        Task AddAsync(TagDto dto);
        Task UpdateAsync(TagDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
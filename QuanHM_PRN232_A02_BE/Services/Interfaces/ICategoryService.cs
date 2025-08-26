using QuanHM_PRN232_A02_BE.DTOs;

namespace QuanHM_PRN232_A02_BE.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(short id);
        Task AddAsync(CategoryDto dto);
        Task UpdateAsync(CategoryDto dto);
        Task<bool> DeleteAsync(short id);
    }
}
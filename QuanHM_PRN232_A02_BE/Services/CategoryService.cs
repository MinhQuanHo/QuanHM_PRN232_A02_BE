using QuanHM_PRN232_A02_BE.Data.Interfaces;
using QuanHM_PRN232_A02_BE.DTOs;
using QuanHM_PRN232_A02_BE.Models;
using QuanHM_PRN232_A02_BE.Services.Interfaces;

namespace QuanHM_PRN232_A02_BE.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        public CategoryService(ICategoryRepository repo) { _repo = repo; }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
            => (await _repo.GetAllAsync()).Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName ?? "",
                CategoryDesciption = c.CategoryDesciption ?? "",
                IsActive = c.IsActive
            });

        public async Task<CategoryDto?> GetByIdAsync(short id)
        {
            var c = await _repo.GetByIdAsync(id);
            return c == null ? null : new CategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName ?? "",
                CategoryDesciption = c.CategoryDesciption ?? "",
                IsActive = c.IsActive
            };
        }

        public async Task AddAsync(CategoryDto dto)
        {
            var entity = new Category
            {
                CategoryId = dto.CategoryId, // nếu Identity thì bỏ
                CategoryName = dto.CategoryName,
                CategoryDesciption = dto.CategoryDesciption,
                IsActive = dto.IsActive
            };
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(CategoryDto dto)
        {
            var c = await _repo.GetByIdAsync(dto.CategoryId);
            if (c == null) return;
            c.CategoryName = dto.CategoryName;
            c.CategoryDesciption = dto.CategoryDesciption;
            c.IsActive = dto.IsActive;
            await _repo.UpdateAsync(c);
            await _repo.SaveAsync();
        }

        public async Task<bool> DeleteAsync(short id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return false;
            if (c.NewsArticles != null && c.NewsArticles.Any()) return false;
            await _repo.DeleteAsync(c);
            await _repo.SaveAsync();
            return true;
        }
    }
}

using QuanHM_PRN232_A02_BE.Data.Interfaces;
using QuanHM_PRN232_A02_BE.DTOs;
using QuanHM_PRN232_A02_BE.Models;
using QuanHM_PRN232_A02_BE.Services.Interfaces;

namespace QuanHM_PRN232_A02_BE.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repo;
        public TagService(ITagRepository repo) { _repo = repo; }

        public async Task<IEnumerable<TagDto>> GetAllAsync()
            => (await _repo.GetAllAsync()).Select(t => new TagDto
            {
                TagId = t.TagId,
                TagName = t.TagName,
                Note = t.Note
            });

        public async Task<TagDto?> GetByIdAsync(int id)
        {
            var t = await _repo.GetByIdAsync(id);
            return t == null ? null : new TagDto { TagId = t.TagId, TagName = t.TagName, Note = t.Note };
        }

        public async Task AddAsync(TagDto dto)
        {
            var entity = new Tag { TagId = dto.TagId, TagName = dto.TagName, Note = dto.Note };
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(TagDto dto)
        {
            var t = await _repo.GetByIdAsync(dto.TagId);
            if (t == null) return;
            t.TagName = dto.TagName;
            t.Note = dto.Note;
            await _repo.UpdateAsync(t);
            await _repo.SaveAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var t = await _repo.GetByIdAsync(id);
            if (t == null) return false;
            await _repo.DeleteAsync(t);
            await _repo.SaveAsync();
            return true;
        }
    }
}

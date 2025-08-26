using Microsoft.EntityFrameworkCore;
using QuanHM_PRN232_A02_BE.Data.Interfaces;
using QuanHM_PRN232_A02_BE.DTOs;
using QuanHM_PRN232_A02_BE.Models;
using QuanHM_PRN232_A02_BE.Services.Interfaces;

namespace QuanHM_PRN232_A02_BE.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepo;
        private readonly IAccountRepository _accRepo;

        public NewsService(INewsRepository newsRepo, IAccountRepository accRepo)
        {
            _newsRepo = newsRepo;
            _accRepo = accRepo;
        }

        public async Task<(IEnumerable<NewsDto> items, int totalCount)> SearchAsync(
            string? keyword, short? categoryId, short? createdBy,
            int page, int pageSize, string sortBy, string sortOrder)
        {
            var (entities, totalCount) = await _newsRepo.SearchAsync(keyword, categoryId, createdBy, page, pageSize, sortBy, sortOrder);

            var dtos = entities.Select(n => new NewsDto
            {
                NewsArticleId = n.NewsArticleId,
                NewsTitle = n.NewsTitle,
                Headline = n.Headline,
                NewsContent = n.NewsContent,
                NewsSource = n.NewsSource,
                CategoryId = n.CategoryId,
                NewsStatus = n.NewsStatus,
                CreatedDate = n.CreatedDate
            });

            return (dtos, totalCount);
        }

        public async Task<IEnumerable<NewsDto>> GetPublicAsync()
        {
            var all = await _newsRepo.FindAsync(n => n.NewsStatus == true);
            return all.Select(n => new NewsDto
            {
                NewsArticleId = n.NewsArticleId,
                NewsTitle = n.NewsTitle,
                Headline = n.Headline ?? "",
                NewsContent = n.NewsContent,
                NewsSource = n.NewsSource,
                CategoryId = n.CategoryId,
                NewsStatus = n.NewsStatus,
                CreatedDate = n.CreatedDate
            });
        }

        public async Task<IEnumerable<NewsDto>> GetMyNewsAsync(string email)
        {
            var acc = await _accRepo.GetByEmailAsync(email);
            if (acc == null) return Enumerable.Empty<NewsDto>();

            var list = await _newsRepo.FindAsync(n => n.CreatedById == acc.AccountId);
            return list.Select(n => new NewsDto
            {
                NewsArticleId = n.NewsArticleId,
                NewsTitle = n.NewsTitle,
                Headline = n.Headline ?? "",
                NewsContent = n.NewsContent,
                NewsSource = n.NewsSource,
                CategoryId = n.CategoryId,
                NewsStatus = n.NewsStatus,
                CreatedDate = n.CreatedDate
            });
        }

        public async Task<NewsDto?> GetByIdAsync(string id, string email)
        {
            var acc = await _accRepo.GetByEmailAsync(email);
            if (acc == null) return null;

            var items = await _newsRepo.FindAsync(n => n.NewsArticleId == id && n.CreatedById == acc.AccountId);
            var n = items.FirstOrDefault();
            if (n == null) return null;

            return new NewsDto
            {
                NewsArticleId = n.NewsArticleId,
                NewsTitle = n.NewsTitle,
                Headline = n.Headline ?? "",
                NewsContent = n.NewsContent,
                NewsSource = n.NewsSource,
                CategoryId = n.CategoryId,
                NewsStatus = n.NewsStatus,
                CreatedDate = n.CreatedDate
            };
        }

        public async Task AddAsync(NewsDto dto, string email)
        {
            var acc = await _accRepo.GetByEmailAsync(email);
            if (acc == null) throw new InvalidOperationException("User not found");

            var entity = new NewsArticle
            {
                NewsArticleId = dto.NewsArticleId, // nếu DB tự sinh thì bỏ
                NewsTitle = dto.NewsTitle,
                Headline = dto.Headline,
                NewsContent = dto.NewsContent,
                NewsSource = dto.NewsSource,
                CategoryId = dto.CategoryId,
                NewsStatus = dto.NewsStatus ?? false,
                CreatedDate = DateTime.Now,
                CreatedById = acc.AccountId
            };
            await _newsRepo.AddAsync(entity);
            await _newsRepo.SaveAsync();
        }

        public async Task<bool> UpdateAsync(string id, NewsDto dto, string email)
        {
            var acc = await _accRepo.GetByEmailAsync(email);
            if (acc == null) return false;

            var items = await _newsRepo.FindAsync(n => n.NewsArticleId == id && n.CreatedById == acc.AccountId);
            var n = items.FirstOrDefault();
            if (n == null) return false;

            n.NewsTitle = dto.NewsTitle;
            n.Headline = dto.Headline;
            n.NewsContent = dto.NewsContent;
            n.NewsSource = dto.NewsSource;
            n.CategoryId = dto.CategoryId;
            n.NewsStatus = dto.NewsStatus;
            n.ModifiedDate = DateTime.Now;
            n.UpdatedById = acc.AccountId;

            await _newsRepo.UpdateAsync(n);
            await _newsRepo.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string id, string email)
        {
            var acc = await _accRepo.GetByEmailAsync(email);
            if (acc == null) return false;

            var items = await _newsRepo.FindAsync(n => n.NewsArticleId == id && n.CreatedById == acc.AccountId);
            var n = items.FirstOrDefault();
            if (n == null) return false;

            await _newsRepo.DeleteAsync(n);
            await _newsRepo.SaveAsync();
            return true;
        }
    }
}

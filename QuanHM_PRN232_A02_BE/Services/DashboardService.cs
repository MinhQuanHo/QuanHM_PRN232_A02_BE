using Microsoft.EntityFrameworkCore;
using QuanHM_PRN232_A02_BE.Data.Interfaces;
using QuanHM_PRN232_A02_BE.DTOs;
using QuanHM_PRN232_A02_BE.Models;
using QuanHM_PRN232_A02_BE.Services.Interfaces;

namespace QuanHM_PRN232_A02_BE.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly INewsRepository _newsRepo;
        private readonly IAccountRepository _accRepo;

        public DashboardService(INewsRepository newsRepo, IAccountRepository accRepo)
        {
            _newsRepo = newsRepo;
            _accRepo = accRepo;
        }

        public async Task<DashboardDto> GetStatisticsAsync()
        {
            var allNews = await _newsRepo.GetAllAsync();
            var allAccs = await _accRepo.GetAllAsync();

            return new DashboardDto
            {
                TotalNews = allNews.Count(),
                PublishedNews = allNews.Count(n => n.NewsStatus == true),
                DraftNews = allNews.Count(n => n.NewsStatus == false),

                TotalUsers = allAccs.Count(),
                StaffCount = allAccs.Count(a => a.AccountRole == 1),
                LecturerCount = allAccs.Count(a => a.AccountRole == 2),

                TopCategories = allNews
                    .GroupBy(n => n.CategoryId)
                    .OrderByDescending(g => g.Count())
                    .Take(5)
                    .Select(g => $"Category {g.Key}: {g.Count()} news")
                    .ToList(),

                LatestNews = allNews
                    .OrderByDescending(n => n.CreatedDate)
                    .Take(5)
                    .Select(n => n.NewsTitle ?? "")
                    .ToList()
            };
        }
    }
}

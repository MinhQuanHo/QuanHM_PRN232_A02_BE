using QuanHM_PRN232_A02_BE.DTOs;

namespace QuanHM_PRN232_A02_BE.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetStatisticsAsync();
    }
}

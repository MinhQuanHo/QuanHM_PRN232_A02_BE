using QuanHM_PRN232_A02_BE.DTOs;
using QuanHM_PRN232_A02_BE.Models;

namespace QuanHM_PRN232_A02_BE.Services.Interfaces
{
    public interface IAccountService
    {
        // Auth
        Task<SystemAccount?> LoginAsync(LoginDto dto);
        Task<bool> RegisterAsync(RegisterDto dto);

        // CRUD
        Task<IEnumerable<SystemAccount>> GetAllAsync();
        Task<SystemAccount?> GetByIdAsync(int id);
        Task<bool> AddAsync(AccountDto dto);     
        Task<bool> UpdateAsync(AccountDto dto);  
        Task<bool> DeleteAsync(int id);

        // Role management
        Task<bool> UpdateRoleAsync(int accountId, int newRoleId);
    }
}

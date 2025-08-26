using QuanHM_PRN232_A02_BE.Models;

namespace QuanHM_PRN232_A02_BE.Data.Interfaces
{
    public interface IAccountRepository
    {
        // Auth
        Task<SystemAccount?> GetByEmailPasswordAsync(string email, string password);
        Task<SystemAccount?> GetByEmailAsync(string email);

        // CRUD
        Task<bool> AddAsync(SystemAccount account);
        Task<bool> UpdateAsync(SystemAccount account);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<SystemAccount>> GetAllAsync();
        Task<SystemAccount?> GetByIdAsync(int id);

        // Role management
        Task<bool> UpdateRoleAsync(int accountId, int newRoleId);
    }
}

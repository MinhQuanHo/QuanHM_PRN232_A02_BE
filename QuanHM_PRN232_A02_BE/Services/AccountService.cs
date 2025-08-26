using QuanHM_PRN232_A02_BE.Data.Interfaces;
using QuanHM_PRN232_A02_BE.DTOs;
using QuanHM_PRN232_A02_BE.Models;
using QuanHM_PRN232_A02_BE.Services.Interfaces;

namespace QuanHM_PRN232_A02_BE.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<SystemAccount?> LoginAsync(LoginDto dto)
        {
            return await _repo.GetByEmailPasswordAsync(dto.Email, dto.Password);
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var existing = await _repo.GetByEmailAsync(dto.Email);
            if (existing != null) return false;

            var account = new SystemAccount
            {
                AccountEmail = dto.Email,
                AccountPassword = dto.Password,
                AccountName = dto.FullName,
                AccountRole = 2
            };

            return await _repo.AddAsync(account);
        }

        public async Task<IEnumerable<SystemAccount>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<SystemAccount?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<bool> AddAsync(AccountDto dto)
        {
            var account = new SystemAccount
            {
                AccountEmail = dto.AccountEmail,
                AccountPassword = dto.AccountPassword,
                AccountName = dto.AccountName,
                AccountRole = dto.AccountRole
            };
            return await _repo.AddAsync(account);
        }

        public async Task<bool> UpdateAsync(AccountDto dto)
        {
            var account = new SystemAccount
            {
                AccountId = dto.AccountId,
                AccountEmail = dto.AccountEmail,
                AccountPassword = dto.AccountPassword,
                AccountName = dto.AccountName,
                AccountRole = dto.AccountRole
            };
            return await _repo.UpdateAsync(account);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<bool> UpdateRoleAsync(int accountId, int newRoleId)
        {
            return await _repo.UpdateRoleAsync(accountId, newRoleId);
        }
    }
}

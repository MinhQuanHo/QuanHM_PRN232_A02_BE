using Microsoft.EntityFrameworkCore;
using QuanHM_PRN232_A02_BE.Data.Interfaces;
using QuanHM_PRN232_A02_BE.Models;

namespace QuanHM_PRN232_A02_BE.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FUNewsManagementContext _context;

        public AccountRepository(FUNewsManagementContext context)
        {
            _context = context;
        }

        public async Task<SystemAccount?> GetByEmailPasswordAsync(string email, string password)
        {
            return await _context.SystemAccounts
                .FirstOrDefaultAsync(a => a.AccountEmail == email && a.AccountPassword == password);
        }

        public async Task<SystemAccount?> GetByEmailAsync(string email)
        {
            return await _context.SystemAccounts.FirstOrDefaultAsync(a => a.AccountEmail == email);
        }

        public async Task<bool> AddAsync(SystemAccount account)
        {
            _context.SystemAccounts.Add(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(SystemAccount account)
        {
            var existing = await _context.SystemAccounts.FindAsync(account.AccountId);
            if (existing == null) return false;

            existing.AccountEmail = account.AccountEmail;
            existing.AccountPassword = account.AccountPassword;
            existing.AccountName = account.AccountName;
            existing.AccountRole = account.AccountRole;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var account = await _context.SystemAccounts.FindAsync(id);
            if (account == null) return false;

            _context.SystemAccounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SystemAccount>> GetAllAsync()
        {
            return await _context.SystemAccounts.ToListAsync();
        }

        public async Task<SystemAccount?> GetByIdAsync(int id)
        {
            return await _context.SystemAccounts.FindAsync(id);
        }

        public async Task<bool> UpdateRoleAsync(int accountId, int newRoleId)
        {
            var acc = await _context.SystemAccounts.FindAsync(accountId);
            if (acc == null) return false;
            acc.AccountRole = newRoleId;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

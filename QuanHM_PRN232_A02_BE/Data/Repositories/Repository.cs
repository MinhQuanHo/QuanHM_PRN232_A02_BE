using Microsoft.EntityFrameworkCore;
using QuanHM_PRN232_A02_BE.Data.Interfaces;
using QuanHM_PRN232_A02_BE.Models;
using System.Linq.Expressions;

namespace QuanHM_PRN232_A02_BE.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly FUNewsManagementContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(FUNewsManagementContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<T?> GetByIdAsync(object id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await _dbSet.Where(predicate).ToListAsync();
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public async Task UpdateAsync(T entity) => _dbSet.Update(entity);
        public async Task DeleteAsync(T entity) => _dbSet.Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
using QuanHM_PRN232_A02_BE.Data.Interfaces;
using QuanHM_PRN232_A02_BE.Models;

namespace QuanHM_PRN232_A02_BE.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(FUNewsManagementContext ctx) : base(ctx) { }
    }
}

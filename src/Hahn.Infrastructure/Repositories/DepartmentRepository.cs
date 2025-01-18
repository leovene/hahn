using Hahn.Domain.Entities;
using Hahn.Domain.Interfaces;
using Hahn.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Hahn.Infrastructure.Repositories
{
    public class DepartmentRepository : Repository<DepartmentEntity>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<DepartmentEntity?> GetByDepartmentIdAsync(int departmentId)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        }
    }
}

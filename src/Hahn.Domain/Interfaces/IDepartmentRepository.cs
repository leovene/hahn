using Hahn.Domain.Entities;

namespace Hahn.Domain.Interfaces
{
    public interface IDepartmentRepository : IRepository<DepartmentEntity>
    {
        public Task<DepartmentEntity?> GetByDepartmentIdAsync(int departmentId);
    }
}

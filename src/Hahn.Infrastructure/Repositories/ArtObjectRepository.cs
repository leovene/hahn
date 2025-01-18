using Hahn.Domain.Entities;
using Hahn.Domain.Interfaces;
using Hahn.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Hahn.Infrastructure.Repositories
{
    public class ArtObjectRepository : Repository<ArtObjectEntity>, IArtObjectRepository
    {
        public ArtObjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<ArtObjectEntity>> GetArtObjectsByDepartmentAsync(int departmentId, int offset, int limit)
        {
            return await _dbSet
                .Where(a => a.DepartmentId == departmentId) 
                .Skip(offset) 
                .Take(limit)  
                .ToListAsync();
        }

        public async Task<int> GetArtObjectsCountByDepartmentAsync(int departmentId)
        {
            return await _dbSet.CountAsync(a => a.DepartmentId == departmentId);
        }

        public async Task<ArtObjectEntity?> GetByObjectIdAsync(int objectId)
        {
            return await _context.ArtObjects.FirstOrDefaultAsync(d => d.ObjectId == objectId);
        }
    }
}

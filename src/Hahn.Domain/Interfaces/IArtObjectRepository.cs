using Hahn.Domain.Entities;

namespace Hahn.Domain.Interfaces
{
    public interface IArtObjectRepository : IRepository<ArtObjectEntity>
    {
        public Task<List<ArtObjectEntity>> GetArtObjectsByDepartmentAsync(int departmentId, int offset, int limit);
        public Task<int> GetArtObjectsCountByDepartmentAsync(int departmentId);
        public Task<ArtObjectEntity?> GetByObjectIdAsync(int objectId);
    }
}

using Hahn.Application.DTOs;

namespace Hahn.Application.Interfaces
{
    public interface IArtObjectService
    {
        public Task AddArtObjectAsync(ObjectDetailResponseDto artObjectDto, int departmentId);
        public Task UpdateArtObjectAsync(int id, ObjectDetailResponseDto artObjectDto);
        public Task DeleteArtObjectAsync(int id);
        public Task<List<ObjectDetailResponseDto>> GetAllArtObjectsAsync();
        public Task<ObjectDetailResponseDto?> GetArtObjectByIdAsync(int id);
        public Task<List<ObjectDetailResponseDto>> GetArtObjectsByDepartmentAsync(int departmentId, int offset, int limit);
        public Task<int> GetArtObjectsCountByDepartmentAsync(int departmentId);
    }
}

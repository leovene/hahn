using Hahn.Application.DTOs;

namespace Hahn.Application.Interfaces
{
    public interface IMuseumApiService
    {
        public Task<List<DepartmentResponseDto>> GetDepartmentsAsync();
        public Task<ObjectIdsResponseDto> GetObjectIdsByDepartmentIdAsync(int departmentId);
        public Task<ObjectIdsResponseDto> GetObjectIdsByDepartmentAsync(List<int> departmentIds);
        public Task<ObjectDetailResponseDto> GetObjectDetailAsync(int objectId);
    }
}

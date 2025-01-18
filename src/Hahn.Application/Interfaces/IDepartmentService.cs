using Hahn.Application.DTOs;

namespace Hahn.Application.Interfaces
{
    public interface IDepartmentService
    {
        public Task AddDepartmentAsync(DepartmentResponseDto departmentDto);
        public Task UpdateDepartmentAsync(int id, DepartmentResponseDto departmentDto);
        public Task DeleteDepartmentAsync(int id);
        public Task<List<DepartmentResponseDto>> GetAllDepartmentsAsync();
        public Task<DepartmentResponseDto?> GetDepartmentByIdAsync(int id);
    }
}

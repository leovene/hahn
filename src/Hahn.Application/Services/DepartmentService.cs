using AutoMapper;
using Hahn.Application.DTOs;
using Hahn.Application.Interfaces;
using Hahn.Domain.Entities;
using Hahn.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hahn.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(IDepartmentRepository repository, IMapper mapper, ILogger<DepartmentService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddDepartmentAsync(DepartmentResponseDto departmentDto)
        {
            _logger.LogInformation("Adding a new department: {DepartmentName}", departmentDto.DisplayName);

            try
            {
                var department = _mapper.Map<DepartmentEntity>(departmentDto);
                await _repository.AddAsync(department);
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Department added successfully: {DepartmentName}", departmentDto.DisplayName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding a department: {DepartmentName}", departmentDto.DisplayName);
                throw;
            }
        }

        public async Task UpdateDepartmentAsync(int id, DepartmentResponseDto departmentDto)
        {
            _logger.LogInformation("Updating department with ID: {DepartmentId}", id);

            try
            {
                var existingDepartment = await _repository.GetByIdAsync(id);
                if (existingDepartment == null)
                {
                    _logger.LogWarning("Department with ID {DepartmentId} not found.", id);
                    throw new KeyNotFoundException("Department not found.");
                }

                existingDepartment.DisplayName = departmentDto.DisplayName ?? existingDepartment.DisplayName;
                _repository.Update(existingDepartment);
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Department with ID {DepartmentId} updated successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating department with ID: {DepartmentId}", id);
                throw;
            }
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            _logger.LogInformation("Deleting department with ID: {DepartmentId}", id);

            try
            {
                var department = await _repository.GetByIdAsync(id);
                if (department == null)
                {
                    _logger.LogWarning("Department with ID {DepartmentId} not found.", id);
                    throw new KeyNotFoundException("Department not found.");
                }

                _repository.Delete(department);
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Department with ID {DepartmentId} deleted successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting department with ID: {DepartmentId}", id);
                throw;
            }
        }

        public async Task<List<DepartmentResponseDto>> GetAllDepartmentsAsync()
        {
            _logger.LogInformation("Fetching all departments.");

            try
            {
                var departments = await _repository.GetAllAsync();
                _logger.LogInformation("Successfully fetched {Count} departments.", departments.Count());
                return _mapper.Map<List<DepartmentResponseDto>>(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching departments.");
                throw;
            }
        }

        public async Task<DepartmentResponseDto?> GetDepartmentByIdAsync(int id)
        {
            _logger.LogInformation("Fetching department with ID: {DepartmentId}", id);

            try
            {
                var department = await _repository.GetByIdAsync(id);
                if (department == null)
                {
                    _logger.LogWarning("Department with ID {DepartmentId} not found.", id);
                    return null;
                }

                _logger.LogInformation("Successfully fetched department with ID: {DepartmentId}", id);
                return _mapper.Map<DepartmentResponseDto?>(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching department with ID: {DepartmentId}", id);
                throw;
            }
        }
    }
}
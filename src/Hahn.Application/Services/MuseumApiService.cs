using Hahn.Application.DTOs;
using Hahn.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Hahn.Application.Services
{
    public class MuseumApiService : IMuseumApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MuseumApiService> _logger;

        private const string DepartmentsUri = "departments";
        private const string ObjectsUri = "objects";

        public MuseumApiService(HttpClient httpClient, ILogger<MuseumApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<DepartmentResponseDto>> GetDepartmentsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(DepartmentsUri);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to fetch departments. Status code: {StatusCode}", response.StatusCode);
                    return new List<DepartmentResponseDto>();
                }

                var data = await response.Content.ReadFromJsonAsync<DepartmentListResponseDto>();
                return data?.Departments ?? new List<DepartmentResponseDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching departments.");
                throw;
            }
        }

        public async Task<ObjectDetailResponseDto> GetObjectDetailAsync(int objectId)
        {
            try
            {
                var uri = $"{ObjectsUri}/{objectId}";
                var response = await _httpClient.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to fetch object details for ID {ObjectId}. Status code: {StatusCode}", objectId, response.StatusCode);
                    return new ObjectDetailResponseDto();
                }

                var data = await response.Content.ReadFromJsonAsync<ObjectDetailResponseDto>();
                return data ?? new ObjectDetailResponseDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching object details for ID {ObjectId}.", objectId);
                throw;
            }
        }

        public async Task<ObjectIdsResponseDto> GetObjectIdsByDepartmentAsync(List<int> departmentIds)
        {
            try
            {
                var departmentQuery = string.Join("|", departmentIds);
                var uri = $"{ObjectsUri}?departmentIds={departmentQuery}";
                var response = await _httpClient.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to fetch object IDs. Status code: {StatusCode}", response.StatusCode);
                    return new ObjectIdsResponseDto();
                }

                var data = await response.Content.ReadFromJsonAsync<ObjectIdsResponseDto>();
                return data ?? new ObjectIdsResponseDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching object IDs.");
                throw;
            }
        }

        public async Task<ObjectIdsResponseDto> GetObjectIdsByDepartmentIdAsync(int departmentId)
        {
            try
            {
                var uri = $"{ObjectsUri}?departmentIds={departmentId}";
                var response = await _httpClient.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to fetch object IDs. Status code: {StatusCode}", response.StatusCode);
                    return new ObjectIdsResponseDto();
                }

                var data = await response.Content.ReadFromJsonAsync<ObjectIdsResponseDto>();
                return data ?? new ObjectIdsResponseDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching object IDs.");
                throw;
            }
        }
    }
}

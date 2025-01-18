using AutoMapper;
using Hahn.Application.DTOs;
using Hahn.Application.Interfaces;
using Hahn.Domain.Entities;
using Hahn.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hahn.Application.Services
{
    public class ArtObjectService : IArtObjectService
    {
        private readonly IArtObjectRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ArtObjectService> _logger;

        public ArtObjectService(IArtObjectRepository repository, IMapper mapper, ILogger<ArtObjectService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddArtObjectAsync(ObjectDetailResponseDto artObjectDto, int departmentId)
        {
            _logger.LogInformation("Adding a new art object: {Title}", artObjectDto.Title);

            try
            {
                var artObject = _mapper.Map<ArtObjectEntity>(artObjectDto);
                artObject.DepartmentId = departmentId;

                await _repository.AddAsync(artObject);
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Art object added successfully: {Title}", artObjectDto.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding art object: {Title}", artObjectDto.Title);
                throw;
            }
        }

        public async Task UpdateArtObjectAsync(int id, ObjectDetailResponseDto artObjectDto)
        {
            _logger.LogInformation("Updating art object with ID: {ArtObjectId}", id);

            try
            {
                var existingArtObject = await _repository.GetByIdAsync(id);
                if (existingArtObject == null)
                {
                    _logger.LogWarning("Art object with ID {ArtObjectId} not found.", id);
                    throw new KeyNotFoundException("Art object not found.");
                }

                _mapper.Map(artObjectDto, existingArtObject);
                _repository.Update(existingArtObject);
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Art object with ID {ArtObjectId} updated successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating art object with ID: {ArtObjectId}", id);
                throw;
            }
        }

        public async Task DeleteArtObjectAsync(int id)
        {
            _logger.LogInformation("Deleting art object with ID: {ArtObjectId}", id);

            try
            {
                var artObject = await _repository.GetByIdAsync(id);
                if (artObject == null)
                {
                    _logger.LogWarning("Art object with ID {ArtObjectId} not found.", id);
                    throw new KeyNotFoundException("Art object not found.");
                }

                _repository.Delete(artObject);
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Art object with ID {ArtObjectId} deleted successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting art object with ID: {ArtObjectId}", id);
                throw;
            }
        }

        public async Task<List<ObjectDetailResponseDto>> GetAllArtObjectsAsync()
        {
            _logger.LogInformation("Fetching all art objects.");

            try
            {
                var artObjects = await _repository.GetAllAsync();
                _logger.LogInformation("Successfully fetched {Count} art objects.", artObjects.Count());
                return _mapper.Map<List<ObjectDetailResponseDto>>(artObjects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching art objects.");
                throw;
            }
        }

        public async Task<ObjectDetailResponseDto?> GetArtObjectByIdAsync(int id)
        {
            _logger.LogInformation("Fetching art object with ID: {ArtObjectId}", id);

            try
            {
                var artObject = await _repository.GetByIdAsync(id);
                if (artObject == null)
                {
                    _logger.LogWarning("Art object with ID {ArtObjectId} not found.", id);
                    return null;
                }

                _logger.LogInformation("Successfully fetched art object with ID: {ArtObjectId}", id);
                return _mapper.Map<ObjectDetailResponseDto?>(artObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching art object with ID: {ArtObjectId}", id);
                throw;
            }
        }

        public async Task<List<ObjectDetailResponseDto>> GetArtObjectsByDepartmentAsync(int departmentId, int offset, int limit)
        {
            var artObjects = await _repository.GetArtObjectsByDepartmentAsync(departmentId, offset, limit);
            return _mapper.Map<List<ObjectDetailResponseDto>>(artObjects);
        }

        public async Task<int> GetArtObjectsCountByDepartmentAsync(int departmentId)
        {
            return await _repository.GetArtObjectsCountByDepartmentAsync(departmentId);
        }
    }
}
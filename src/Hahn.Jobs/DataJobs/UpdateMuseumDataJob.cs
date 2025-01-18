using AutoMapper;
using Hahn.Application.Interfaces;
using Hahn.Domain.Entities;
using Hahn.Domain.Interfaces;
using Hahn.Jobs.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hahn.Jobs.DataJobs
{
    public class UpdateMuseumDataJob : IUpdateMuseumDataJob
    {
        private readonly IMuseumApiService _museumApiService;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IArtObjectRepository _artObjectRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateMuseumDataJob> _logger;

        public UpdateMuseumDataJob(
            IMuseumApiService museumApiService,
            IDepartmentRepository departmentRepository,
            IArtObjectRepository artObjectRepository,
            IMapper mapper,
            ILogger<UpdateMuseumDataJob> logger)
        {
            _museumApiService = museumApiService;
            _departmentRepository = departmentRepository;
            _artObjectRepository = artObjectRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task ExecuteAsync()
        {
            try
            {
                _logger.LogInformation("Job started: Updating museum data.");

                var departments = await _museumApiService.GetDepartmentsAsync();
                foreach (var departmentDto in departments)
                {
                    var department = await _departmentRepository.GetByDepartmentIdAsync(departmentDto.DepartmentId);
                    if (department == null)
                    {
                        var departmentEntity = _mapper.Map<DepartmentEntity>(departmentDto);
                        await _departmentRepository.AddAsync(departmentEntity);
                    }
                }

                await _departmentRepository.SaveChangesAsync();

                var allDepartments = await _departmentRepository.GetAllAsync();
                foreach (var department in allDepartments)
                {
                    _logger.LogInformation($"Processing department ID: {department.DepartmentId} - {department.DisplayName}");

                    var departmentExists = await _departmentRepository.GetByDepartmentIdAsync(department.DepartmentId);
                    if (departmentExists == null)
                    {
                        _logger.LogError($"The department with ID {department.DepartmentId} does not exist. Skipping department.");
                        continue;
                    }

                    _logger.LogInformation($"Fetching object IDs for department ID: {department.DepartmentId}");
                    var objectIdsResponse = await _museumApiService.GetObjectIdsByDepartmentIdAsync(department.DepartmentId);

                    if (objectIdsResponse.Total == 0 || objectIdsResponse.ObjectIDs == null)
                    {
                        _logger.LogWarning($"No objects found for department ID: {department.DepartmentId}");
                        continue;
                    }

                    foreach (var objectId in objectIdsResponse.ObjectIDs.Take(100))
                    {
                        var artObject = await _artObjectRepository.GetByObjectIdAsync(objectId);
                        if (artObject == null)
                        {
                            var objectDetail = await _museumApiService.GetObjectDetailAsync(objectId);
                            var artObjectEntity = _mapper.Map<ArtObjectEntity>(objectDetail);
                            artObjectEntity.DepartmentId = department.DepartmentId;

                            _logger.LogInformation($"Saving art object ID: {objectId} with DepartmentId: {artObjectEntity.DepartmentId}");
                            await _artObjectRepository.AddAsync(artObjectEntity);
                        }
                    }

                    await _artObjectRepository.SaveChangesAsync();
                }

                _logger.LogInformation("Job completed: Museum data updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating museum data.");
            }
        }
    }
}

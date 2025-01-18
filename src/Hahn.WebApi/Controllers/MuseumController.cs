using Hahn.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MuseumController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IArtObjectService _artObjectService;

        public MuseumController(IDepartmentService departmentService, IArtObjectService artObjectService)
        {
            _departmentService = departmentService;
            _artObjectService = artObjectService;
        }

        /// <summary>
        /// Obtém a lista de departamentos.
        /// </summary>
        [HttpGet("departments")]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var departments = await _departmentService.GetAllDepartmentsAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Failed to fetch departments.", Error = ex.Message });
            }
        }


        [HttpGet("art-objects")]
        public async Task<IActionResult> GetArtObjectsByDepartment([FromQuery] int departmentId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(new { Message = "Page and pageSize must be greater than 0." });
            }

            try
            {
                var offset = (page - 1) * pageSize;
                var artObjects = await _artObjectService.GetArtObjectsByDepartmentAsync(departmentId, offset, pageSize);

                var totalRecords = await _artObjectService.GetArtObjectsCountByDepartmentAsync(departmentId);
                Response.Headers.Add("X-Total-Count", totalRecords.ToString());

                return Ok(artObjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Failed to fetch art objects.", Error = ex.Message });
            }
        }
    }
}

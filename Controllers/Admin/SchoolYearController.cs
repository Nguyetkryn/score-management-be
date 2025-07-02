using Microsoft.AspNetCore.Mvc;
using score_management_be.DTOs;
using score_management_be.Services;
using Microsoft.AspNetCore.Http;

namespace score_management_be.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolYearController : ControllerBase
    {
        private readonly ISchoolYearService _schoolYearService;
        public SchoolYearController (ISchoolYearService schoolYearService)
        {
            _schoolYearService = schoolYearService;
        }

        [HttpGet("list-years")]
        public async Task<ActionResult<IEnumerable<SchoolYearDto>>> GetListYears()
        {
            var existingListYears = await _schoolYearService.GetAllYears();
            return Ok(existingListYears);
        }

        [HttpGet("get-{id}-year")]
        public async Task<ActionResult<SchoolYearDto>> GetYear(Guid id)
        {
            try
            {
                var existingYear = await _schoolYearService.GetYearById(id);
                return Ok(existingYear);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
        }

        [HttpPost("create-new-school-year")]
        public async Task<ActionResult<SchoolYearDto>> CreateNewSchoolYear([FromBody] SchoolYearDto schoolYearDto)
        {
            if (schoolYearDto == null || string.IsNullOrWhiteSpace(schoolYearDto.Year))
            {
                return BadRequest("Invalid role data.");
            }
            try
            {
                var createSchoolYear = await _schoolYearService.CreateSchoolYearAsync(schoolYearDto);
                return Ok(createSchoolYear);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("update-{id}-year")]
        public async Task<ActionResult<SchoolYearDto>> UpdateSchoolYear(Guid id, [FromBody] SchoolYearDto schoolYearDto)
        {
            if (id != schoolYearDto.Id)
            {
                return BadRequest("Role ID mismatch.");
            }
            try
            {
                var existingSchoolYear = await _schoolYearService.UpdateSchoolYearAsync(schoolYearDto);
                return Ok(existingSchoolYear);
            } catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
        }

        [HttpDelete("delete-{id}-year")]
        public async Task<ActionResult> DeleteYear(Guid id)
        {
            try
            {
                var existingSchoolYear = await _schoolYearService.DeleteSchoolYearAsync(id);
                return Ok("Delete successfully!");
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest();
            }
        }
    }
}

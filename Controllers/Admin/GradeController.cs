using Microsoft.AspNetCore.Mvc;
using score_management_be.DTOs;
using score_management_be.Services;

namespace score_management_be.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        public GradeController(IGradeService gradeService) { 
            _gradeService = gradeService;
        }

        [HttpGet("get-list-grades")]
        public async Task<ActionResult<IEnumerable<GradeDto>>> GetAllGrades(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize
        )
        {
            // xét điều kiện nếu là null thì sao?
            var grades = await _gradeService.GetListAllGradesAsync(pageNumber, pageSize);
            return Ok(grades);
        }

        [HttpGet("get-grade-{id}")]
        public async Task<ActionResult<GradeDto>> GetGradeById(Guid id)
        {
            try
            {
                var grade = await _gradeService.GetGradeById(id);
                return Ok(grade);
            }  
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("create-grade")]
        public async Task<ActionResult<GradeDto>> CreateNewGrade([FromBody] string dto) {
            if (dto == null || string.IsNullOrWhiteSpace(dto)) {
                return BadRequest("Invalid grade data.");
            }
            try
            {
                var createGrade = await _gradeService.CreateGradeAsync(dto);
                return CreatedAtAction(nameof(GetAllGrades), new {createGrade.Id}, createGrade);
            } 
            catch (InvalidOperationException ex)
            {
                return Conflict(ex);
            }
        }

        [HttpPut("update-grade-{id}")]
        public async Task<ActionResult<GradeDto>> UpdateGrade(Guid id, [FromBody] GradeDto gradeDto)
        {
            if (id != gradeDto.Id)
            {
                return BadRequest("Grade ID mismatch.");
            }
            try
            {
                var updateGrade = await _gradeService.UpdateGradeAsync(gradeDto);
                return Ok(updateGrade);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("delete-grade-{id}")]
        public async Task<ActionResult> DeleteGrade (Guid id)
        {
            try
            {
                var deleteGrade = await _gradeService.DeleteGradeAsync(id);
                return Ok(deleteGrade);
            } 
            catch (KeyNotFoundException)
            {
                return BadRequest("Error delete grade.");
            }
        }
    }
}

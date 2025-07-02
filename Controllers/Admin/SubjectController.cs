using Microsoft.AspNetCore.Mvc;
using score_management_be.DTOs;
using score_management_be.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace score_management_be.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet("get-all-subjects")]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjects(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize
        )
        {
            var subjects = await _subjectService.GetAllSubjectAsync(pageNumber, pageSize);
            return Ok(subjects);
        }

        [HttpGet("get-subject-{id}")]
        public async Task<ActionResult<SubjectDto>> GetSubject(Guid id)
        {
            try
            {
                var subject = await _subjectService.GetSubjectById(id);
                return Ok(subject);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<SubjectDto>> CreateSubject([FromBody] string subjectDto)
        {
            if (subjectDto == null || string.IsNullOrWhiteSpace(subjectDto)) {
                return BadRequest("Invalid subject data.");
            }
            try
            {
                var createdSubject = await _subjectService.CreateSubjectAsync(subjectDto);
                return CreatedAtAction(nameof(GetSubjects), new { id = createdSubject.Id}, createdSubject);
            }
            catch (InvalidOperationException ex)
            {
                // Return 409 Bad Request if the subject name already exists
                return Conflict(ex.Message);
            }
        }

        [HttpPut("update-subject-{id}")]
        public async Task<ActionResult<SubjectDto>> UpdateSubject(Guid id, [FromBody] SubjectDto subjectDto)
        {
            if (id != subjectDto.Id)
            {
                return BadRequest("Subject ID mismatch.");
            }

            try
            {
                var updateSubject = await _subjectService.UpdateSubjectAsync(subjectDto);
                return Ok(updateSubject);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpDelete("delete-subject-{id}")]
        public async Task<ActionResult> DeleteSubject(Guid id)
        {
            try
            {
                await _subjectService.DeleteSubjectAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return BadRequest("Error delete subject.");
            }
        }
    }
}

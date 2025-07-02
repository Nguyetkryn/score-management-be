using Microsoft.AspNetCore.Mvc;
using score_management_be.DTOs;
using score_management_be.Services;

namespace score_management_be.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomService _service;
        public ClassroomController(IClassroomService service) { 
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try
            {
                return Ok(await _service.GetAllAsync());
            }
            catch (InvalidOperationException ex) { 
                return NotFound(new {message = ex.Message});
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) {
            try
            {
                return Ok(await _service.GetByIdAsync(id));
            } catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClassroomDto dto) {
            try
            {
                var result = await _service.CreateAsync(dto);
                return Ok(new {message = result});
            } catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ClassroomDto dto) {
            if (id != dto.Id) {
                return BadRequest("Classroom ID mismatch.");
            }
            try
            {
                var result = await _service.UpdateAsync(dto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {
            try
            {
                await _service.DeleteAsync(id);
                return Ok(new { message = "Classroom deleted successfully." });
            }
            catch (KeyNotFoundException ex) {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}

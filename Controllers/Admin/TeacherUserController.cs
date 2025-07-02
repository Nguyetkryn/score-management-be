using Microsoft.AspNetCore.Mvc;
using score_management_be.DTOs;
using score_management_be.Services;

namespace score_management_be.Controllers.Admin
{
    [Route("/api/[controller]")]
    [ApiController]
    public class TeacherUserController : ControllerBase
    {
        private readonly ITeacherUserService _teacherUserService;

        public TeacherUserController (ITeacherUserService teacherUserService)
        {
            _teacherUserService = teacherUserService;
        }

        [HttpGet("get-{id}")]
        public async Task<ActionResult<IEnumerable<TeacherUserDto>>> GetTeacherByUserId (Guid id)
        {
            if (id == null)
            {
                return BadRequest("Invalid data.");
            }
            var teacher = await _teacherUserService.GetTeacherByUserId(id);
            return Ok(teacher);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using score_management_be.DTOs;
using score_management_be.Services;

namespace score_management_be.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterService _semesterService;
        public SemesterController( ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        [HttpGet("get-list-semester")]
        public async Task<ActionResult<IEnumerable<SemesterDto>>> GetListSemester()
        {
            var semesters = await _semesterService.GetSemesterListAsync();
            return Ok(semesters);
        }


    }
}

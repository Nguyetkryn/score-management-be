using Microsoft.AspNetCore.Mvc;
using score_management_be.DTOs;
using score_management_be.Services;

namespace score_management_be.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet("get-roles-by-user/userId-{userId}")]
        public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetRolesByUser(Guid userId)
        {
            if (userId == null)
            {
                return BadRequest("Invalid data.");
            }
            var roleList = await _userRoleService.GetListRoleByUserId(userId);
            return Ok(roleList);
        }

        [HttpGet("get-users-by-role/roleId-{roleId}")]
        public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetUsersByRoleId (Guid roleId)
        {
            if (roleId == null)
            {
                return BadRequest("Invalid data.");
            }
            var users = await _userRoleService.GetListUserByRoleId(roleId);
            return Ok(users);
        }
    }
}

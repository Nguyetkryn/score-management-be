using Microsoft.AspNetCore.Mvc;
using score_management_be.DTOs;
using score_management_be.Services;

namespace score_management_be.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("get-all-roles")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var roles = await _roleService.GetAllRoleAsync(pageNumber, pageSize);
            return Ok(roles);
        }

        [HttpGet("get-{id}")]
        public async Task<ActionResult<RoleDto>> GetRole(Guid id)
        {
            try
            {
                var role = await _roleService.GetRoleById(id);
                return Ok(role);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<RoleDto>> CreateRole([FromBody] string roleName)
        {
            if (roleName == null || string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Invalid role data.");
            }
            try
            {
                var createRole = await _roleService.CreateRoleAsync(roleName);
                return CreatedAtAction(nameof(GetRole), new { id = createRole.Id }, createRole);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("update-{id}")]
        public async Task<ActionResult<RoleDto>> UpdateRole(Guid id, [FromBody] string roleName)
        {
            try
            {
                // Create a RoleDto with the ID from route and name from body
                var roleDto = new RoleDto
                {
                    Id = id,
                    RoleName = roleName
                };

                // Call the service method
                var updatedRole = await _roleService.UpdateRoleAsync(roleDto);
                return Ok(updatedRole);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // delete thành công cứ để ok chứ không cần noContent
        // bị lỗi thì phải trả 400 bad request, 404 là not found là đường dẫn không tồn tại thôi
        [HttpDelete("delete-{id}")]
        public async Task<ActionResult> DeleteRole(Guid id)
        {
            try
            {
                await _roleService.DeleteRoleAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return BadRequest("Error delete role.");
            }
        }
    }
}

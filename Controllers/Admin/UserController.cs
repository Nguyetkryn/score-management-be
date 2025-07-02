using Microsoft.AspNetCore.Mvc;
using score_management_be.DTOs;
using score_management_be.Models;
using score_management_be.Services;

namespace score_management_be.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        public UserController(IUserService userService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
        }

        [HttpGet("get-all-users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var userList = await _userService.GetAllUserAsync(pageNumber, pageSize);
                return Ok(userList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user-{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                return Ok(user);
            } catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("create-new-user")]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserCreateDto userDto)
        {
            try
            {
                var createUser = await _userService.CreateUserAsync(userDto);
                await _userRoleService.AssignRoleAsync(createUser.Id, userDto.RoleIds);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("create-user-role")]
        //public async Task<ActionResult<UserRoleDto>> CreateUserRole([FromBody] UserRoleDto userRoleDto)
        //{
        //    try
        //    {
        //        var createUserRole = await _userService.CreateUserRoleAsync(userRoleDto.UserId, userRoleDto.RoleId);
        //        return Ok();
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound();
        //    }
        //}

        [HttpPut("update-{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser ([FromBody] UserDto userDto)
        {
            try
            {
                var updateUser = await _userService.UpdateUserAsync(userDto);
                return Ok(updateUser);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Soft-delete
        [HttpDelete("delete-{id}")]
        public async Task<ActionResult> RemoveUser(Guid id)
        {
            try
            {
                await _userService.RemoveUserAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // Hard-delete
        [HttpDelete("delete/delete-{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

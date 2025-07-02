using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using score_management_be.DTOs;
using score_management_be.Models;
using score_management_be.Repositories;
using score_management_be.Utils;
using System.Security.AccessControl;

namespace score_management_be.Services
{
    public interface IUserService
    {
        Task<Pagination<UserDto>> GetAllUserAsync (int pageNumber, int pageSize);
        Task<UserDto> GetUserById (Guid id);
        Task<UserDto> CreateUserAsync (UserCreateDto userDto);
        Task<UserDto> UpdateUserAsync (UserDto user);
        Task<bool> RemoveUserAsync(Guid userId);
        Task<bool> DeleteUserAsync (Guid id);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IRoleService _roleService;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRoleService _userRoleService;

        public UserService(
            IUserRepository userRepository, 
            ILogger<UserService> logger, 
            IRoleService roleService, 
            IUserRoleRepository userRoleRepository,
            IUserRoleService userRoleService
        ) 
        {
            _userRepository = userRepository;
            _logger = logger;
            _roleService = roleService;
            _userRoleRepository = userRoleRepository;
            _userRoleService = userRoleService;
        }

        // GET ALL DATA WITH PAGINATION
        public async Task<Pagination<UserDto>> GetAllUserAsync(int pageNumber, int pageSize)
        {
            try
            {
                if (_userRepository == null)
                {
                    throw new NullReferenceException("_userRepository has already initalized.");
                }

                var users = _userRepository.FindAll();
                if (users == null)
                {
                    throw new NullReferenceException("Users is null.");
                }

                var pageResult = await users.ToPagedListAsync(pageNumber, pageSize);

                var userDtos = new List<UserDto>();

                foreach (var user in pageResult.Items)
                {
                    var roles = await _userRoleService.GetListRoleByUserId(user.Id);


                    userDtos.Add(new UserDto
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Address = user.Address,
                        Birth = user.Birth,
                        Email = user.Email,
                        EndDate = user.EndDate,
                        FullName = user.FullName,
                        Gender = user.Gender,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        ProfileImageUrl = user.ProfileImageUrl,
                        StartDate = user.StartDate,
                        UserRoles = roles,
                        TeacherUserId = user.TeacherUser?.TeacherId,
                        StudentUserId = user.StudentUser?.StudentId,
                    });
                }

                return new Pagination<UserDto>
                {
                    Items = userDtos.ToList(),
                    TotalCount = pageResult.TotalCount,
                    PageNumber = pageResult.PageNumber,
                    PageSize = pageResult.PageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching roles. Page: {PageNumber}, Size: {PageSize}", pageNumber, pageSize);
                throw;
            }
        }

        // GET USER BY ID
        public async Task<UserDto> GetUserById(Guid id)
        {
            var user = await _userRepository.FindByCondition(u => u.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            var roles = await _userRoleService.GetListRoleByUserId(user.Id);

            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Address = user.Address,
                Birth = user.Birth,
                Email = user.Email,
                EndDate = user.EndDate,
                FullName = user.FullName,
                Gender = user.Gender,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                ProfileImageUrl = user.ProfileImageUrl,
                StartDate = user.StartDate,
                UserRoles = roles,
                TeacherUserId = user.TeacherUser?.TeacherId,
                StudentUserId = user.StudentUser?.StudentId,
            };
        }

        // CREATE NEW USER
        public async Task<UserDto> CreateUserAsync(UserCreateDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto), "Invalid user data.");
            }
                      
            var existingUser = await _userRepository.GetUserNameAsync(userDto.UserName);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"User with '{userDto.UserName}' already exists.");
            }

            var user = new Models.User
            {
                Id = new Guid(),
                UserName = userDto.UserName,
                Password = userDto.Password,
                Address = userDto.Address,
                Birth = userDto.Birth,
                Email = userDto.Email,
                EndDate = userDto.EndDate,
                FullName = userDto.FullName,
                Gender = userDto.Gender,
                PhoneNumber = userDto.PhoneNumber,
                ProfileImageUrl = userDto.ProfileImageUrl,
                IsStatus = userDto.IsStatus,
                StartDate = userDto.StartDate,
                CreatedBy = userDto.CreatedBy,
                CreatedDate = userDto.CreatedDate
            };

            var createUser = await _userRepository.CreateAsync(user);

            return new UserDto
            {
                Id = createUser.Id
            };
        }

        // CREATE USER ROLE
        //public async Task<UserRoleDto> CreateUserRoleAsync(Guid userId, Guid roleId)
        //{
        //    var existingUser = await _userRepository.FindByCondition(u => u.Id == userId).FirstOrDefaultAsync();
        //    if (existingUser == null) 
        //    {
        //        throw new KeyNotFoundException();
        //    }

        //    var existingRole = await _roleService.GetRoleById(roleId);
        //    if (existingRole == null)
        //    {
        //        throw new KeyNotFoundException();
        //    }

        //    var userRole = new Models.UserRole
        //    {
        //        UserId = userId,
        //        RoleId = roleId,
        //    };

        //    var createUserRole = await _userRoleRepository.CreateAsync(userRole);

        //    return new UserRoleDto
        //    {
        //        UserId = createUserRole.UserId,
        //        RoleId = createUserRole.RoleId,
        //    };
        //}

        // UPDATE EXIST USER
        public async Task<UserDto> UpdateUserAsync(UserDto userDto)
        {
            var user = await _userRepository.GetUserNameAsync(userDto.UserName);
            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            if (userDto.UserRoles == null)
            {
                throw new ArgumentNullException(nameof(userDto.UserRoles), "Invalid role data.");
            }

            var validRole = new List<string>();
            foreach (var roleName in userDto.UserRoles)
            {
                var existingRole = await _roleService.GetRoleById(roleName.RoleId);
                if (existingRole == null)
                {
                    throw new InvalidOperationException($"Invalid role: {existingRole}");
                }

                validRole.Add(existingRole.RoleName);
            }

            if (userDto.UserName != user.UserName)
            {
                var existingUser = await _userRepository.GetUserNameAsync(userDto.UserName);
                if (existingUser != null)
                {
                    throw new ArgumentException("A user with this username already exists.");
                }
            }

            user.UserName = userDto.UserName;
            await _userRepository.UpdateAsync(user);
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Address = user.Address,
                Birth = user.Birth,
                Email = user.Email,
                EndDate = user.EndDate,
                FullName = user.FullName,
                Gender = user.Gender,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                ProfileImageUrl = user.ProfileImageUrl,
                StartDate = user.StartDate,
                ModifiedBy = user.ModifiedBy,
                ModifiedDate = user.ModifiedDate,
                UserRoles = user.UserRoles?.Select(ur => new UserRoleDto
                {
                    UserId = ur.UserId,
                    RoleId = ur.RoleId
                }).ToList(),
                TeacherUserId = user.TeacherUser?.TeacherId,
                StudentUserId = user.StudentUser?.StudentId,
            };
        }

        // SOFT-DELETE
        public async Task<bool> RemoveUserAsync(Guid userId)
        {
            var user = await _userRepository.FindByCondition(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            user.IsStatus = false;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        // HARD-DELETE (REMOVE IN THE DATABASE)
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.FindByCondition(u => u.Id == id).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            await _userRepository.RemoveAsync(user.Id);
            return true;
        }
    }
}

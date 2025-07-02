using Microsoft.EntityFrameworkCore;
using score_management_be.DTOs;
using score_management_be.Models;
using score_management_be.Repositories;
using score_management_be.Utils;

namespace score_management_be.Services
{
    public interface IUserRoleService
    {
        Task<List<UserRoleDto>> GetListRoleByUserId(Guid userId);
        Task<List<string>> GetRoleNameByUserId(Guid userId);
        Task<List<UserRoleDto>> GetListUserByRoleId(Guid roleId);
        Task<string> AssignRoleAsync(Guid userId, List<Guid> roleIds);
    }
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<List<UserRoleDto>> GetListRoleByUserId(Guid userId)
        {
            var userRoles = await _userRoleRepository.FindByCondition(ur => ur.UserId == userId)
                .Include(ur => ur.Role)
                .Select(ur => new UserRoleDto
                {
                    UserId = ur.UserId,
                    RoleId = ur.RoleId,
                    RoleName = ur.Role.RoleName, 
                    // The error occurred because UserRole entity doesn't have RoleName property directly -
                    // it needs to be accessed through the Role navigation property using ur.Role.RoleName.
                })
                .ToListAsync();

            return userRoles;
        }

        public async Task<List<string>> GetRoleNameByUserId(Guid userId)
        {
            var roleNames = await _userRoleRepository.FindByCondition(ur => ur.UserId == userId)
                .Include(ur => ur.Role)
                .Select(ur => ur.Role.RoleName)
                .ToListAsync();
            return roleNames;
        }

        public async Task<List<UserRoleDto>> GetListUserByRoleId(Guid roleId)
        {
            var userListByRoleId = await _userRoleRepository.FindByCondition(r => r.RoleId == roleId)
                .Include(r => r.User)
                .Select(r => new UserRoleDto
                {
                    UserId = r.UserId,
                    RoleId = r.RoleId,
                    RoleName = r.Role.RoleName,
                })
                .ToListAsync();
            return userListByRoleId;
        }

        public async Task<string> AssignRoleAsync(Guid userId, List<Guid> roleIds)
        {
            var userRoles = roleIds.Select(roleId => new UserRole
            {
                UserId = userId,
                RoleId = roleId
            });

            await _userRoleRepository.CreateRangeAsync(userRoles);
            return "Create successful";
        }

    }
}

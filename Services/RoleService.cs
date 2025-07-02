using score_management_be.DTOs;
using Microsoft.EntityFrameworkCore;
using score_management_be.Repositories;
using score_management_be.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using score_management_be.Models;

namespace score_management_be.Services
{
    public interface IRoleService
    {
        Task<Pagination<RoleDto>> GetAllRoleAsync(int pageNumber, int pageSize);
        Task<RoleDto> GetRoleById(Guid id);
        Task<RoleDto> GetRoleByNameAsync(string name);
        Task<RoleDto> CreateRoleAsync(string role);
        Task<RoleDto> UpdateRoleAsync(RoleDto roleDto);
        Task<bool> DeleteRoleAsync(Guid id);
    }

    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleService> _logger;

        // mỗi lần thêm gì đó vào lớp này thì lại phải thêm vào constructor rất tù
        // thay vào đó tìm hiểu unit of work
        // ví dụ : tạo 1 repository manager quản lý tất cả repository
        // đăng ký addScoped với Program là được , nó sẽ tạo instance rồi mà không phải dùng constructor thế này nữa
        public RoleService(IRoleRepository roleRepository, ILogger<RoleService> logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<RoleDto> CreateRoleAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException(nameof(roleName), "Invaild role data.");
            }

            var existingRole = await _roleRepository.GetRoleByNameAsync(roleName);
            if (existingRole != null)
            {
                throw new InvalidOperationException($"Role with name '{roleName}' already exists.");
            }

            var role = new Models.Role
            {
                Id = Guid.NewGuid(),  // Nếu cần tạo ID thủ công, nếu không thì DB tự tạo.
                RoleName = roleName,
            };
            var createdRole = await _roleRepository.CreateAsync(role);

            return new RoleDto
            {
                Id = role.Id,
                RoleName = role.RoleName
            };
        }

        public async Task<bool> DeleteRoleAsync(Guid id)
        {
            var role = await _roleRepository.FindByCondition(r => r.Id == id).FirstOrDefaultAsync();
            if (role == null) { throw new KeyNotFoundException(); }
            await _roleRepository.RemoveAsync(role.Id);
            return true;
        }

        // roles mà null là chết code ở đây luôn này =))
        // if roles mà không null thì mới select
        // roles mà null thì trả ra null thôi
        public async Task<Pagination<RoleDto>> GetAllRoleAsync(int pageNumber, int pageSize)
        {
            try
            {
                if (_roleRepository == null)
                    throw new NullReferenceException("_roleRepository chưa được khởi tạo.");

                var roles = _roleRepository.FindAll(); // Ensure this is IQueryable
                if (roles == null)
                    throw new NullReferenceException("Danh sách roles trả về null.");

                var pagedResult = await roles.ToPagedListAsync(pageNumber, pageSize);

                return new Pagination<RoleDto>
                {
                    Items = pagedResult.Items.Select(role => new RoleDto
                    {
                        Id = role.Id,
                        RoleName = role.RoleName
                    }).ToList(),
                    TotalCount = pagedResult.TotalCount,
                    PageNumber = pagedResult.PageNumber,
                    PageSize = pagedResult.PageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching roles. Page: {PageNumber}, Size: {PageSize}", pageNumber, pageSize);
                throw; // Keep throwing to ensure the caller is aware of the failure
            }
        }

        // GET ROLE BY ID
        public async Task<RoleDto> GetRoleById(Guid id)
        {
            var role = await _roleRepository.FindByCondition(r => r.Id == id).FirstOrDefaultAsync();
            if (role == null) { throw new KeyNotFoundException(); }
            return new RoleDto
            {
                Id = role.Id,
                RoleName = role.RoleName
            };
        }

        public async Task<RoleDto> GetRoleByNameAsync(string roleName)
        {
            if (roleName == null || string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException(nameof(roleName), "Invaild role data.");
            }

            var existingRole = await _roleRepository.GetRoleByNameAsync(roleName);
            if (existingRole == null)
            {
                throw new InvalidOperationException($"Invalid role: {roleName}");
            }

            return new RoleDto
            {
                Id = existingRole.Id,
                RoleName = existingRole.RoleName
            };
        }

        public async Task<RoleDto> UpdateRoleAsync(RoleDto roleDto)
        {
            var role = await _roleRepository.FindByCondition(r => r.Id == roleDto.Id).FirstOrDefaultAsync();
            if (role == null) { throw new KeyNotFoundException(); }
                        
            if (role.RoleName != roleDto.RoleName)
            {
                var existingRoleName = await _roleRepository.GetRoleByNameAsync(roleDto.RoleName);
                if (existingRoleName != null)
                {
                    throw new ArgumentException("A role with this name already exists.");
                }
            }

            role.RoleName = roleDto.RoleName;
            await _roleRepository.UpdateAsync(role);
            return new RoleDto
            {
                RoleName = role.RoleName
            };

        }
    }
}
  
using Microsoft.EntityFrameworkCore;
using score_management_be.Data;
using score_management_be.Models;

namespace score_management_be.Repositories
{

    // có thể viết thế này cho nhanh nha
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role?> GetRoleByNameAsync(string roleName);
    }

    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
        }
    }
}

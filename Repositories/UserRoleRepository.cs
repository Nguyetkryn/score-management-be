using Microsoft.EntityFrameworkCore;
using score_management_be.Data;
using score_management_be.Models;

namespace score_management_be.Repositories
{
    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {
        Task CreateRangeAsync(IEnumerable<UserRole> entities);
    }

    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext context) : base(context) { }

        public async Task CreateRangeAsync(IEnumerable<UserRole> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentException("No roles to assign.");

            await _context.UserRoles.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

    }
}

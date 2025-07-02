using Microsoft.EntityFrameworkCore;
using score_management_be.Data;
using score_management_be.Models;

namespace score_management_be.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserNameAsync(string userName);
    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<User?> GetUserNameAsync(string userName)
        {
           return await _context.Users.FirstOrDefaultAsync(r => r.UserName == userName);
        }
    }
}

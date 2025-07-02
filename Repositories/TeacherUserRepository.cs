using score_management_be.Data;
using score_management_be.Models;

namespace score_management_be.Repositories
{
    public interface ITeacherUserRepository : IBaseRepository<TeacherUser> { }
    public class TeacherUserRepository : BaseRepository<TeacherUser>, ITeacherUserRepository
    {
        public TeacherUserRepository (ApplicationDbContext context) : base (context) { }
    }
}

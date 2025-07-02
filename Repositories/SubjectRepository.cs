using score_management_be.Data;
using score_management_be.Models;
using Microsoft.EntityFrameworkCore;


namespace score_management_be.Repositories
{
    public interface ISubjectRepository : IBaseRepository<Subject>
    {
        Task<Subject?> GetSubjectByNameAsync(string subjectName);
    }

    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Subject?> GetSubjectByNameAsync(string subjectName)
        {
            return await _context.Subjects.FirstOrDefaultAsync(r => r.SubjectName == subjectName);
        }
    }
}

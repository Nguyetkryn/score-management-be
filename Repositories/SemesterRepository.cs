using Microsoft.EntityFrameworkCore;
using score_management_be.Data;
using score_management_be.Models;

namespace score_management_be.Repositories
{
    public interface ISemesterRepository : IBaseRepository<Semester>
    {
        Task<Semester?> GetSemesterByNameAsync(string name);
        Task<Semester> CreateAsync(Semester semester);
    }
    public class SemesterRepository : BaseRepository<Semester>, ISemesterRepository
    {
        public SemesterRepository(ApplicationDbContext context) : base(context)
        {
        }
        
        public async Task<Semester?> GetSemesterByNameAsync(string name)
        {
            return await _context.Semesters.FirstOrDefaultAsync(s => s.Name == name);
        }


        public override async Task<Semester> CreateAsync(Semester semester)
        {
            var createSemester = await GetSemesterByNameAsync(semester.Name);
            var existingSemester = await _context.Semesters.FirstOrDefaultAsync(s => s.Name == semester.Name && s.SchoolYear.Id == semester.SchoolYearId);
            if (existingSemester != null)
            {
                throw new InvalidOperationException($"A semester with the name '{semester.Name}' already exists for the school year '{semester.SchoolYearId}'.");
            }
            return await base.CreateAsync(semester);
        }

    }
}

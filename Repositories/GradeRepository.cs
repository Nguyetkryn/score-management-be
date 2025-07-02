using Microsoft.EntityFrameworkCore;
using score_management_be.Data;
using score_management_be.Models;

namespace score_management_be.Repositories
{
    public interface IGradeRepository : IBaseRepository<Grade>
    {
        Task<Grade?> GetGradeByNameAsync (string gradeName);
        Task<Grade> CreateAsync (Grade grade);
    }
    public class GradeRepository : BaseRepository<Grade>, IGradeRepository
    {
        public GradeRepository (ApplicationDbContext _context) : base (_context) { }

        public async Task<Grade?> GetGradeByNameAsync (string gradeName)
        {
            return await _context.Grades.FirstOrDefaultAsync(x => x.GradeName == gradeName);
        }

        public override async Task<Grade> CreateAsync(Grade grade)
        {
            var createGrade = await GetGradeByNameAsync(grade.GradeName);
            if (createGrade != null) {
                throw new InvalidOperationException($"Grade with name '{grade.GradeName}' already exists.");
            }
            return await base.CreateAsync(grade);
        }
    }
}

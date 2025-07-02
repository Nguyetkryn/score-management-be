using Microsoft.EntityFrameworkCore;
using score_management_be.Data;
using score_management_be.Models;

namespace score_management_be.Repositories
{
    public interface IClassroomRepository : IBaseRepository<Classroom>
    {
        Task<Classroom?> GetByNameAsync(string className);
        Task<bool> ExistsAsync(Guid id);
    }
    public class ClassroomRepository : BaseRepository<Classroom>, IClassroomRepository
    {
        public ClassroomRepository (ApplicationDbContext context) : base (context) { }

        public async Task<Classroom?> GetByNameAsync(string className)
        {
            return await _context.Classrooms.FirstOrDefaultAsync(c => c.ClassName == className);
        }

        public override async Task<Classroom> CreateAsync(Classroom classroom)
        {
            if (await GetByNameAsync(classroom.ClassName) != null)
            {
                throw new InvalidOperationException($"Classroom with name '{classroom.ClassName}' already exists.");
            }
            return await base.CreateAsync(classroom);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Classrooms.AnyAsync(c => c.Id == id);
        }
    }
}

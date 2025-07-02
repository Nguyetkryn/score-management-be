using Microsoft.EntityFrameworkCore;
using score_management_be.Data;
using score_management_be.Models;

namespace score_management_be.Repositories
{
    public interface ISchoolYearRepository : IBaseRepository<SchoolYear>
    {
        Task<SchoolYear?> GetSchoolYearByYearAsync(string year);
        Task<SchoolYear> CreateAsync(SchoolYear schoolYear);
    }

    public class SchoolYearRepository : BaseRepository<SchoolYear>, ISchoolYearRepository
    {
        public SchoolYearRepository(ApplicationDbContext context) : base(context) { }

        public async Task<SchoolYear?> GetSchoolYearByYearAsync(string year)
        {
            return await _context.SchoolYears.FirstOrDefaultAsync(s => s.Year == year);
        }

        public override async Task<SchoolYear> CreateAsync(SchoolYear schoolYear)
        {
            var existingYear = await GetSchoolYearByYearAsync(schoolYear.Year);
            if (existingYear != null) {
                throw new InvalidOperationException($"School year with name '{schoolYear.Year}' already exists.");
            }
            return await base.CreateAsync(schoolYear);
        }
    }
}
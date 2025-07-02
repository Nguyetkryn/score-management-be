using Microsoft.EntityFrameworkCore;
using score_management_be.DTOs;
using score_management_be.Models;
using score_management_be.Repositories;
namespace score_management_be.Services
{
    public interface ISemesterService
    {
        Task<List<SemesterDto>> GetSemesterListAsync();
        Task<SemesterDto> GetSemesterById(Guid id);
        Task<SemesterDto> CreateSemesterAsync(SemesterDto semesterDto);
        Task CreateSemesterBySchoolYearIdAsync(Semester semester);

    }
    public class SemesterService : ISemesterService
    {
        public readonly ISemesterRepository _semesterRepository;

        public SemesterService (ISemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }

        public Task<SemesterDto> CreateSemesterAsync(SemesterDto semesterDto)
        {
            throw new NotImplementedException();
        }

        public async Task CreateSemesterBySchoolYearIdAsync(Semester semester)
        {
            // Validate dữ liệu
            if (semester == null || string.IsNullOrEmpty(semester.Name))
            {
                throw new ArgumentNullException("Invaild semester data.");
            }

            if (semester.SchoolYear == null)
            {
                throw new ArgumentNullException("School year is required.");
            }
            
            // Lưu vào cơ sở dữ liệu
            //_semesterRepository.Semesters.Add(semester);
            //await _context.SaveChangesAsync();
            await _semesterRepository.CreateAsync(semester);
        }

        public Task<SemesterDto> GetSemesterById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SemesterDto>> GetSemesterListAsync()
        {
            var semesters = await _semesterRepository.FindAll().ToListAsync();
            if (semesters != null)
            {
                return semesters.Select(s => new SemesterDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    SchoolYearId = s.SchoolYearId
                }).ToList();
            }
            return null;
        }
    }
}

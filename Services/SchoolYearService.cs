using Microsoft.EntityFrameworkCore;
using score_management_be.DTOs;
using score_management_be.Models;
using score_management_be.Repositories;

namespace score_management_be.Services
{
    public interface ISchoolYearService
    {
        Task<List<SchoolYearDto>> GetAllYears();
        Task<SchoolYearDto> GetYearById(Guid id);
        Task<string> CreateSchoolYearAsync(SchoolYearDto schoolYearDto);
        Task<string> UpdateSchoolYearAsync(SchoolYearDto schoolYearDto);
        Task<bool> DeleteSchoolYearAsync(Guid id);
    }
    public class SchoolYearService : ISchoolYearService
    {

        public readonly ISchoolYearRepository _schoolYearRepository;
        public readonly ISemesterService _semesterService;

        public SchoolYearService(ISchoolYearRepository schoolYearRepository, ISemesterService semesterService)
        {
            _schoolYearRepository = schoolYearRepository;
            _semesterService = semesterService;
        }

        public async Task<string> CreateSchoolYearAsync(SchoolYearDto schoolYearDto)
        {
            if (schoolYearDto == null || string.IsNullOrWhiteSpace(schoolYearDto.Year)) 
            {
                throw new ArgumentNullException("Invaild school year data.");     
            }
  
            var year = new Models.SchoolYear
            {
                Year = schoolYearDto.Year,
                StartDate = schoolYearDto.StartDate,
                EndDate = schoolYearDto.EndDate,
            };

            var createYear = await _schoolYearRepository.CreateAsync(year);

            if (createYear == null)
            {
                throw new InvalidOperationException("Failed to create a new school year.");
            }

            schoolYearDto.Id = createYear.Id;

            var semesters = new List<Semester>
            {
                new Semester { Name = "First Semester", SchoolYear = createYear },
                new Semester { Name = "Second Semester", SchoolYear = createYear }
            };

            foreach (var semester in semesters)
            {
                if (semester.SchoolYear == null)
                {
                    throw new InvalidOperationException("Semester does not have an associated SchoolYear.");
                }

                await _semesterService.CreateSemesterBySchoolYearIdAsync(semester);
            }

            /*return new SchoolYearDto
            {
                Id = year.Id,
                Year = year.Year,
                StartDate = year.StartDate,
                EndDate = year.EndDate,
            };*/
            return "Create successfully!";
        }

        public async Task<bool> DeleteSchoolYearAsync(Guid id)
        {
            var year = await _schoolYearRepository.FindByCondition(s => s.Id == id).FirstOrDefaultAsync();
            if (year == null)
            {
                throw new KeyNotFoundException();
            }
            await _schoolYearRepository.RemoveAsync(year.Id);
            return true;
        }

        public async Task<List<SchoolYearDto>> GetAllYears()
        {
            var schoolYears = await _schoolYearRepository.FindAll().ToListAsync();
            if (schoolYears != null)
            {
                return schoolYears.Select(sY => new SchoolYearDto
                {
                    Id = sY.Id,
                    Year = sY.Year,
                    StartDate = sY.StartDate,
                    EndDate = sY.EndDate
                }).ToList();
            }
            return null;
        }

        public async Task<SchoolYearDto> GetYearById(Guid id)
        {
            var schoolYear = await _schoolYearRepository.FindByCondition(s => s.Id == id).FirstOrDefaultAsync();
            if (schoolYear == null)
            {
                throw new KeyNotFoundException();
            }
            return new SchoolYearDto
            {
                Id = schoolYear.Id,
                Year = schoolYear.Year
            };
        }

        public async Task<string> UpdateSchoolYearAsync(SchoolYearDto schoolYearDto)
        {
            var schoolYear = await _schoolYearRepository.FindByCondition(s => s.Id == schoolYearDto.Id).FirstOrDefaultAsync();
            if (schoolYear == null)
            {
                throw new KeyNotFoundException();
            }
            var existingSchoolYear = await _schoolYearRepository.GetSchoolYearByYearAsync(schoolYearDto.Year);
            if (existingSchoolYear != null)
            {
                throw new ArgumentException("A school year with this year already exists.");
            }
            /*            return new SchoolYearDto
                        {
                            Id = schoolYear.Id,
                            Year = schoolYear.Year,
                        };*/
            return "Update successfully!";
        }
    }
}

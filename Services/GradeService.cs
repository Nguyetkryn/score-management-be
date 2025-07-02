using Microsoft.EntityFrameworkCore;
using score_management_be.DTOs;
using score_management_be.Repositories;
using score_management_be.Utils;

namespace score_management_be.Services
{
    public interface IGradeService {
        Task<Pagination<GradeDto>> GetListAllGradesAsync(int pageNumber, int pageSize);
        Task<GradeDto> GetGradeById(Guid id);
        Task<GradeDto> CreateGradeAsync (string dto);
        Task<GradeDto> UpdateGradeAsync (GradeDto dto);
        Task<bool> DeleteGradeAsync (Guid id);
    }
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        public GradeService (IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public async Task<GradeDto> CreateGradeAsync(string dto)
        {
            if (string.IsNullOrWhiteSpace(dto)) {
                throw new ArgumentNullException("Invalid grade data.");
            }
            var grade = new Models.Grade
            {
                Id = Guid.NewGuid(),
                GradeName = dto,
            };

            var createGrade = await _gradeRepository.CreateAsync(grade);

            return new GradeDto
            {
                Id = createGrade.Id,
                GradeName = createGrade.GradeName,
            };
        }

        public async Task<bool> DeleteGradeAsync(Guid id)
        {
            var existingGrade = await _gradeRepository.FindByCondition(g =>  g.Id == id).FirstOrDefaultAsync();
            if (existingGrade == null) { 
                throw new KeyNotFoundException();
            }
            await _gradeRepository.RemoveAsync(existingGrade.Id);
            return true;
        }

        public async Task<GradeDto> GetGradeById(Guid id)
        {
            var existingGrade = await _gradeRepository.FindByCondition(g =>  g.Id == id).FirstOrDefaultAsync();
            if (existingGrade == null) {
                throw new KeyNotFoundException();
            }
            return new GradeDto
            {
                Id = existingGrade.Id,
                GradeName = existingGrade.GradeName,
            };
        }

        public async Task<Pagination<GradeDto>> GetListAllGradesAsync(int pageNumber, int pageSize)
        {
            try
            {
                var listGrades = _gradeRepository.FindAll();
                if (listGrades == null)
                    throw new NullReferenceException("List grade is null.");

                var pageResult = await listGrades.ToPagedListAsync(pageNumber, pageSize);

                return new Pagination<GradeDto>
                {
                    Items = pageResult.Items.Select(grade => new GradeDto
                    {
                        Id = grade.Id,
                        GradeName = grade.GradeName
                    }).ToList(),
                    TotalCount = pageResult.TotalCount,
                    PageNumber = pageResult.PageNumber,
                    PageSize = pageResult.PageSize
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message} - {ex.StackTrace}");
                throw; // Để giữ nguyên lỗi gốc cho Stack Trace
            } 
            
        }

        public async Task<GradeDto> UpdateGradeAsync(GradeDto dto)
        {
            var existingGrade = await _gradeRepository.FindByCondition(g => g.Id == dto.Id).FirstOrDefaultAsync();
            if (existingGrade == null)
            {
                throw new KeyNotFoundException();
            }
            
            existingGrade.GradeName = dto.GradeName;
            await _gradeRepository.UpdateAsync(existingGrade);

            return new GradeDto {
                GradeName= dto.GradeName,
            };
        }
    }
}

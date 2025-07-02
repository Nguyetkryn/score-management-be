using Microsoft.EntityFrameworkCore;
using score_management_be.DTOs;
using score_management_be.Models;
using score_management_be.Repositories;

namespace score_management_be.Services
{
    public interface IClassroomService
    {
        Task<List<ClassroomDto>> GetAllAsync();
        Task<ClassroomDto> GetByIdAsync(Guid id);
        Task<string> CreateAsync(ClassroomDto dto);
        Task<ClassroomDto> UpdateAsync (ClassroomDto dto);
        Task DeleteAsync (Guid id);
    }
    public class ClassroomService : IClassroomService
    {
        private readonly IClassroomRepository _repository;
        public ClassroomService(IClassroomRepository repository) {
            _repository = repository;
        }

        public async Task<string> CreateAsync(ClassroomDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.ClassName)) {
                throw new ArgumentNullException("Invaild classroom data");
            }

            if (await _repository.GetByNameAsync(dto.ClassName) != null)
                throw new InvalidOperationException($"Classroom '{dto.ClassName}' already exists.");

            var classroom = new Models.Classroom
            {
                Id = dto.Id,
                ClassName = dto.ClassName,
                NumberOfStudents = dto.NumberOfStudents,
                GradeId = dto.GradeId,
            };
            await _repository.CreateAsync(classroom);
            return "Classroom created successfully!";
        }

        public async Task DeleteAsync(Guid id)
        {
            if(!await _repository.ExistsAsync(id))
                throw new KeyNotFoundException($"Classroom with ID {id} not found.");
            await _repository.RemoveAsync(id);
        }

        public async Task<List<ClassroomDto>> GetAllAsync()
        {
            var classrooms = await _repository.FindAll().ToListAsync();

            if (classrooms == null || !classrooms.Any())
                throw new InvalidOperationException("No classrooms found.");

            return classrooms.Select(c => new ClassroomDto
            {
                Id = c.Id,
                ClassName = c.ClassName,
                NumberOfStudents = c.NumberOfStudents,
                GradeId = c.GradeId,
            }).ToList();
        }

        public async Task<ClassroomDto> GetByIdAsync(Guid id)
        {
            var existingClassroom = await _repository.FindByCondition(c => c.Id == id).FirstOrDefaultAsync();
            if (existingClassroom == null)
            {
                throw new KeyNotFoundException($"Classroom with ID {id} not found.");
            }
            return new ClassroomDto
            {
                Id = existingClassroom.Id,
                ClassName = existingClassroom.ClassName,
                NumberOfStudents = existingClassroom.NumberOfStudents,
                GradeId = existingClassroom.GradeId,
            };
        }

        public async Task<ClassroomDto> UpdateAsync(ClassroomDto dto)
        {
            var existing = await _repository.ExistsAsync(dto.Id);
            if (!existing)
                throw new KeyNotFoundException($"Classroom with ID {dto.Id} not found.");

            var duplicate = await _repository.FindByCondition(c => c.ClassName == dto.ClassName && c.Id != dto.Id).FirstOrDefaultAsync();
            if (duplicate != null)
            {
                throw new ArgumentException("A classroom with this name already exists.");
            }
            var classroom = new Classroom
            {
                Id = dto.Id,
                ClassName = dto.ClassName,
                NumberOfStudents = dto.NumberOfStudents,
                GradeId = dto.GradeId
            };

            await _repository.UpdateAsync(classroom);
            return dto;
        }
    }
}

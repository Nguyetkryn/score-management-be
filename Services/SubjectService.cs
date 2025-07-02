using score_management_be.DTOs;
using score_management_be.Repositories;
using Microsoft.EntityFrameworkCore;
using score_management_be.Utils;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using score_management_be.Repositories;

namespace score_management_be.Services
{
    public interface ISubjectService
    {
        Task<Pagination<SubjectDto>> GetAllSubjectAsync(int pageNumber, int pageSize);
        Task<SubjectDto> GetSubjectById(Guid id);
        Task<SubjectDto> CreateSubjectAsync(string subject);
        Task<SubjectDto> UpdateSubjectAsync(SubjectDto subjectDto);
        Task<bool> DeleteSubjectAsync(Guid id);
    }

    public class SubjectService : ISubjectService
    {
        private readonly ILogger<RoleService> _logger;
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository, ILogger<RoleService> logger)
        {
            _subjectRepository = subjectRepository;
            _logger = logger;
        }

        public async Task<SubjectDto> CreateSubjectAsync(string subjectName)
        {
            if (string.IsNullOrWhiteSpace(subjectName))
            {
                throw new ArgumentNullException("Invalid subject data.");
            }

            var existingSubject = await _subjectRepository.GetSubjectByNameAsync(subjectName);
            if (existingSubject == null)
            {
                throw new InvalidOperationException($"Subject with '{subjectName}' already exists.");
            }

            var subject = new Models.Subject
            {
                Id = Guid.NewGuid(),
                SubjectName = subjectName,
            };

            var createdSubject = await _subjectRepository.CreateAsync(subject);

            return new SubjectDto
            {
                Id = subject.Id,
                SubjectName = subject.SubjectName,
            };
        }

        public async Task<bool> DeleteSubjectAsync(Guid id)
        {
            var subject = await _subjectRepository.FindByCondition(r => r.Id == id).FirstOrDefaultAsync();
            if (subject == null) { throw new KeyNotFoundException(); }
            await _subjectRepository.RemoveAsync(subject.Id);
            return true;
        }

        public async Task<Pagination<SubjectDto>> GetAllSubjectAsync(int pageNumber, int pageSize)
        {
            try
            {            
                if (_subjectRepository == null)
                    throw new NullReferenceException("_subjectRepository chưa được khởi tạo.");

                var subjects = _subjectRepository.FindAll();
                if (subjects == null)
                    throw new NullReferenceException("Danh sach subjects tra ve null.");

                var pageResult = await subjects.ToPagedListAsync(pageNumber, pageSize);

                return new Pagination<SubjectDto>
                {
                    Items = pageResult.Items.Select(subject => new SubjectDto
                    {
                        Id = subject.Id,
                        SubjectName = subject.SubjectName
                    }).ToList(),
                    TotalCount = pageResult.TotalCount,
                    PageNumber = pageResult.PageNumber,
                    PageSize = pageResult.PageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching subjects. Page: {PageNumber}, Size: {PageSize}", pageNumber, pageSize);
                throw;
            }
        }

        public async Task<SubjectDto> GetSubjectById(Guid id)
        {
            var subject = await _subjectRepository.FindByCondition(r => r.Id == id).FirstOrDefaultAsync();
            if (subject == null) { throw new KeyNotFoundException(); }
            return new SubjectDto
            {
                Id = subject.Id,
                SubjectName = subject.SubjectName
            };

        }

        public async Task<SubjectDto> UpdateSubjectAsync(SubjectDto subjectDto)
        {
            var subject = await _subjectRepository.FindByCondition(r => r.Id == subjectDto.Id).FirstOrDefaultAsync();
            if (subject == null) { throw new KeyNotFoundException(); }

            // Check if the subject name already exists
            var existingSubjectName = await _subjectRepository.GetSubjectByNameAsync(subjectDto.SubjectName);
            if (existingSubjectName != null)
            {
                throw new ArgumentException("A subject with this name already exists.");
            }
            
            subject.SubjectName = subjectDto.SubjectName;
            await _subjectRepository.UpdateAsync(subject);
            return new SubjectDto
            {
                SubjectName = subject.SubjectName
            };

        }
    }
}

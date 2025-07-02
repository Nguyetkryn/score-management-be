using Microsoft.EntityFrameworkCore;
using score_management_be.DTOs;
using score_management_be.Repositories;

namespace score_management_be.Services
{
    public interface ITeacherUserService
    {
        Task<TeacherUserDto> GetTeacherByUserId (Guid userId);
        //Task<List<string>> GetRoleNameByUserId(Guid userId);
    }
    public class TeacherUserService : ITeacherUserService
    {
        private readonly ITeacherUserRepository _teacherUserRepository;

        public TeacherUserService (ITeacherUserRepository teacherUserRepository)
        {
            _teacherUserRepository = teacherUserRepository;
        }

        public async Task<TeacherUserDto> GetTeacherByUserId (Guid userId)
        {
            var teacherUser = await _teacherUserRepository.FindByCondition(u => u.UserId == userId).FirstAsync();
            return new TeacherUserDto
            {
                TeacherId = teacherUser.TeacherId,
                UserId = teacherUser.UserId,
                ProfessionalLevel = "",
                IsHomeroomTeacher = false,
                Seniority = 1,
            };
        }
    }
}

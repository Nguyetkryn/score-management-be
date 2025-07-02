using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace score_management_be.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? Birth { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsStatus { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<UserRoleDto>? UserRoles { get; set; }
        public Guid? TeacherUserId { get; set; }
        public Guid? StudentUserId { get; set; }
    }

    public class UserCreateDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? Birth { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsStatus { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<Guid> RoleIds { get; set; }
    }

}

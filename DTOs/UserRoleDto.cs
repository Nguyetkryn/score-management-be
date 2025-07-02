namespace score_management_be.DTOs
{
    public class UserRoleDto
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public string RoleName { get; set; }
    }
}

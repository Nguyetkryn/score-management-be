namespace score_management_be.Models
{
    public class UserRole
    {
        //Foregin keys
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        //Relationship
        public User User { get; set; }
        public Role Role { get; set; }
    }
}

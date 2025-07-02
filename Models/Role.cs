namespace score_management_be.Models
{
    public class Role : BaseModel
    {
        public string RoleName { get; set; }

        //Relationships
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}

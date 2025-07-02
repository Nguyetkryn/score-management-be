namespace score_management_be.Models
{
    public class StudentUser
    {
        //Foregin keys
        public Guid StudentId { get; set; }
        public Guid UserId { get; set; }
        //Relationship
        public Student Student { get; set; }
        public User User { get; set; }
    }
}

namespace score_management_be.Models
{
    public class TeacherUser
    {
        //Foregin keys
        public Guid TeacherId { get; set; }
        public Guid UserId { get; set; }

        //Relationship
        public Teacher Teacher { get; set; }
        public User User { get; set; }
    }
}

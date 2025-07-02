namespace score_management_be.Models
{
    public class TeachingAssignment : BaseModel
    {
        public Guid TeacherId { get; set; }
        public Guid ClassroomId { get; set; }
        public Guid SemesterId { get; set; }

        public Teacher Teacher { get; set; }
        public Classroom Classroom { get; set; }
        public Semester Semester { get; set; }

        //relationship
        public ICollection<LearningOutcomes> LearningOutcomes { get; set; } = new List<LearningOutcomes>();
    }
}

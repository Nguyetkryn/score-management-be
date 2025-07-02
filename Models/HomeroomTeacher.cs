namespace score_management_be.Models
{
    public class HomeroomTeacher : BaseModel
    {
        public Guid TeacherId { get; set; }
        public Guid StudentId { get; set; }
        public Guid SemesterId { get; set; }
        public Guid ClassroomId { get; set; }

        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
        public Semester Semester { get; set; }
        public Classroom Classroom { get; set; }

        public ICollection<ConductAssessment> ConductAssessments { get; set; } = new List<ConductAssessment>();
    }
}

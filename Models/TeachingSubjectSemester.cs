namespace score_management_be.Models
{
    public class TeachingSubjectSemester
    {
        public Guid TeachingSubjectId { get; set; }
        public Guid SemesterId { get; set; }

        public TeachingSubject TeachingSubject { get; set; }
        public Semester Semester { get; set; }
    }
}

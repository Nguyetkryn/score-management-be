namespace score_management_be.Models
{
    public class GradeSubject
    {
        public Guid GradeId { get; set; }
        public Guid SubjectId { get; set; }
        public Grade Grade { get; set; }
        public Subject Subject { get; set; }
    }
}

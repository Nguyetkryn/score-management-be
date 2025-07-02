namespace score_management_be.Models
{
    public class TeachingSubject : BaseModel
    {
        public Guid TeacherId { get; set; }
        public Guid SubjectId { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }

        //Relationship
        public ICollection<TeachingSubjectSemester> TeachingSubjectSemesters { get; set; } = new List<TeachingSubjectSemester>();
    }
}

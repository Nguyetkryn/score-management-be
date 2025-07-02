namespace score_management_be.DTOs
{
    public class SubjectDto
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; }
        public List<Guid> TeacherIds { get; set; } = new List<Guid>();
    }
}

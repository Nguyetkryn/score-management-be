namespace score_management_be.DTOs
{
    public class ClassroomDto
    {
        public Guid Id { get; set; }
        public string ClassName { get; set; }
        public int NumberOfStudents {  get; set; }
        public Guid GradeId { get; set; }
    }
}

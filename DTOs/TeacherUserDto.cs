namespace score_management_be.DTOs
{
    public class TeacherUserDto
    {
        public Guid TeacherId { get; set; }
        public Guid UserId { get; set; }
        public string ProfessionalLevel { get; set; }
        public int? Seniority { get; set; }
        public bool? IsHomeroomTeacher { get; set; }
    }
}

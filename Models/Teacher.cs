namespace score_management_be.Models
{
    public class Teacher : BaseModel
    {

        public string ProfessionalLevel { get; set; }
        public int? Seniority { get; set; }
        public bool? IsHomeroomTeacher { get; set; }

        //relationships
        public TeacherUser TeacherUser { get; set; }
        public ICollection<HomeroomTeacher> HomeroomTeachers { get; set; } = new List<HomeroomTeacher>();
        public ICollection<TeachingSubject> TeachingSubjects { get; set; } = new List<TeachingSubject>();
        public ICollection<TeachingAssignment> TeachingAssignments { get; set; } = new List<TeachingAssignment>();
    }
}

namespace score_management_be.Models
{
    public class Semester : BaseModel
    {
        public string Name { get; set; }

        //Foregin key
        public Guid SchoolYearId { get; set; }

        //relationships
        public SchoolYear SchoolYear { get; set; }
        public ICollection<HomeroomTeacher> HomeroomTeachers { get; set; } = new List<HomeroomTeacher>();
        public ICollection<TeachingSubjectSemester> TeachingSubjectSemesters { get; set; } = new List<TeachingSubjectSemester>();
        public ICollection<TeachingAssignment> TeachingAssignments { get; set; } = new List<TeachingAssignment>();
    }
}

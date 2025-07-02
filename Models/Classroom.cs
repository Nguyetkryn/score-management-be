using System.Diagnostics;

namespace score_management_be.Models
{
    public class Classroom : BaseModel
    {
        public string ClassName { get; set; }
        public int NumberOfStudents { get; set; }

        //Foregin keys
        public Guid GradeId { get; set; }

        //Relationships
        public Grade Grade { get; set; }
        public ICollection<HomeroomTeacher> HomeroomTeachers { get; set; } = new List<HomeroomTeacher>();
        public ICollection<TeachingAssignment> TeachingAssignments { get; set; } = new List<TeachingAssignment>();
    }
}

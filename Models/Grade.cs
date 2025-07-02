namespace score_management_be.Models
{
    public class Grade : BaseModel
    {
        public string GradeName { get; set; }

        //Relationships
        public ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();
        public ICollection<GradeSubject> GradeSubjects { get; set; } = new List<GradeSubject>();
    }
}

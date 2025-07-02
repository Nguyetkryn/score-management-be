namespace score_management_be.Models
{
    public class Subject : BaseModel
    {
        public string SubjectName { get; set; }

        //Relationships
        public ICollection<TeachingSubject> TeachingSubjects { get; set; } = new List<TeachingSubject>();
        public ICollection<LearningOutcomes> LearningOutcomes { get; set; } = new List<LearningOutcomes>();
        public ICollection<GradeSubject> GradeSubjects { get; set; } = new List<GradeSubject>();
    }
}

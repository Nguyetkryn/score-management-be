namespace score_management_be.Models
{
    public class ConductAssessment
    {
        public Guid HomeroomTeacherId { get; set; }
        public Guid ConductId { get; set; }
        public HomeroomTeacher HomeroomTeacher { get; set; }
        public Conduct Conduct { get; set; }
    }
}

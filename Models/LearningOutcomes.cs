using System.ComponentModel.DataAnnotations;

namespace score_management_be.Models
{
    public class LearningOutcomes : BaseModel
    {
        public double? OralTestScore { get; set; }
        public double? TestScore { get; set; }
        public double? MidtermScore { get; set; }
        public double? FinalScore { get; set; }
        public double? OverallScore { get; set; }
        [StringLength(500, ErrorMessage = "OutComeDescription cannot exceed 500 characters.")]
        public string? OutComeDescription { get; set; }

        //Foregin keys
        public Guid TeachingAssignmentId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid StudentId { get; set; }

        //Relationship
        public TeachingAssignment TeachingAssignment { get; set; }
        public Subject Subject { get; set; }
        public Student Student { get; set; }
    }
}

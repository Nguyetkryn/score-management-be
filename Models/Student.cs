using System.ComponentModel.DataAnnotations;

namespace score_management_be.Models
{
    public class Student : BaseModel
    {
        public string? MotherFullName { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? MotherPhoneNumber { get; set; }
        public string? FatherFullName { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? FatherPhoneNumber { get; set; }
        [StringLength(500, ErrorMessage = "Note cannot exceed 500 characters.")]
        public string? Note { get; set; }

        //relationships
        public StudentUser StudentUser { get; set; }
        public ICollection<HomeroomTeacher> HomeroomTeachers { get; set; } = new List<HomeroomTeacher>();
        public ICollection<LearningOutcomes> LearningOutcomes { get; set; } = new List<LearningOutcomes>();
    }
}

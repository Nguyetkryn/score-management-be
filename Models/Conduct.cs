using System.ComponentModel.DataAnnotations;

namespace score_management_be.Models
{
    public class Conduct : BaseModel
    {
        public string ConductName { get; set; }

        //Relationships
        public ICollection<ConductAssessment> ConductAssessments { get; set; } = new List<ConductAssessment>();
    }
}

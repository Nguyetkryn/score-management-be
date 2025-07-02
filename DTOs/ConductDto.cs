namespace score_management_be.DTOs
{
    public class ConductDto
    {
        public string ConductName { get; set; }
        public List<Guid> ConductAssessments { get; set; } = new List<Guid>();
    }
}

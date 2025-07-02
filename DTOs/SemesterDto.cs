namespace score_management_be.DTOs
{
    public class SemesterDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SchoolYearId { get; set; }
    }
}

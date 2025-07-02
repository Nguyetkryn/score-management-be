using System;

namespace score_management_be.DTOs
{
    public class SchoolYearDto
    {
        public Guid Id { get; set; }
        public string Year { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}

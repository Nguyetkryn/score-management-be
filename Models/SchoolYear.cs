namespace score_management_be.Models
{
    public class SchoolYear : BaseModel
    {
        public string Year { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        //relationships1
        public ICollection<Semester> Semesters { get; set; }
    }
}

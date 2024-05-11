namespace Api.Models
{
    public class Department : AbstractDbEntity
    {
        public string CourseDistribution { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int TotalCredits { get; set; }
        public int MinimumCreditPerSem { get; set; }
        public int DepartmentCode { get; set; }

    }
}

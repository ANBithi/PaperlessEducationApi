namespace Api.Models
{

    public class Result : AbstractDbEntity
    {
        public string BelongsTo { get; set; }
        public float QuizMark { get; set; } 
        public float MidMark { get; set; } 
        public float ProjectMark { get; set; } 
        public float AttendanceMark { get; set; } 
        public float AssignmentMark { get; set; } 
        public float FinalMark { get; set; }
        public string Grade { get; set; }
        public double Gpa { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }

    }
}

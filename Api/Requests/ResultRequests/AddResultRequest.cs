namespace Api.Requests.ResultRequests
{
    public class AddResultRequest
    {
        public string BelongsTo { get; set; }
        public float QuizMark { get; set; } = 0;
        public float MidMark { get; set; } = 0;
        public float ProjectMark { get; set; } = 0;
        public float AttendanceMark { get; set; } = 0;
        public float AssignmentMark { get; set; } = 0;
        public float FinalMark { get; set; } = 0;
        public string CourseName { get; set; }
        public string CourseCode { get; set; }

    }
}

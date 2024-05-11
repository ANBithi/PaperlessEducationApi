namespace Api.Models
{

    public enum ActiveStatusEnum
    {
        Online,
        Offline,
    }

    public enum ActiveStatuForTypeEnum
    {
        All,
        Class,
        Exam,
    }
    public class UserActivity : AbstractDbEntity
    {
        public ActiveStatusEnum Status { get; set; }
        public ActiveStatuForTypeEnum ActivityFor { get; set; }
        public string ActivityForId { get; set; }

    }
}



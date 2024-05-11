using Api.Enums;
using Api.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Api.Mapper
{
    public class ExamMapper
    {
        public static void Map()
        {
            // Mapping should be done once. So, checking if the class has
            // already been registered
            if (!BsonClassMap.IsClassMapRegistered(typeof(Exam)))
            {
                BsonClassMap.RegisterClassMap<Exam>(x =>
                {
                    x.AutoMap();
                    x.MapProperty(e => e.AnswerType).SetElementName("answerType");
                    x.MapProperty(e => e.SectionNumber).SetElementName("sectionNumber");
                    x.MapProperty(e => e.SectionId).SetElementName("sectionId").SetSerializer(new StringSerializer(BsonType.ObjectId));
                    x.MapProperty(e => e.CourseName).SetElementName("courseName");
                    x.MapProperty(e => e.CourseCode).SetElementName("courseCode");
                    x.MapProperty(e => e.Duration).SetElementName("duration");
                    x.MapProperty(e => e.ExamTitle).SetElementName("examTitle");
                    x.MapProperty(e => e.StartTime).SetElementName("startTime");
                    x.MapProperty(e => e.TotalMarks).SetElementName("totalMarks");
                    x.MapProperty(e => e.CountPercentile).SetElementName("countPercentile").SetSerializer(new EnumSerializer<CountPercentileEnum>(BsonType.Int32));
                    x.MapProperty(e => e.ExamType).SetElementName("examType").SetSerializer(new EnumSerializer<ExamTypeEnum>(BsonType.Int32));
                    x.MapProperty(e => e.Questions).SetElementName("questions");
                });
            }
        }
    }
}

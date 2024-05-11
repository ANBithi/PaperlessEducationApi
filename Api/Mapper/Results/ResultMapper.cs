using Api.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Api.Mapper.Results
{
    public class ResultMapper
    {
        public static void Map()
        {
            // Mapping should be done once. So, checking if the class has
            // already been registered
            if (!BsonClassMap.IsClassMapRegistered(typeof(Result)))
            {
                BsonClassMap.RegisterClassMap<Result>(x =>
                {
                    x.AutoMap();
                    x.MapProperty(e => e.BelongsTo).SetElementName("belongsTo").SetSerializer(new StringSerializer(BsonType.ObjectId));
                    x.MapProperty(e => e.CourseCode).SetElementName("courseCode");
                    x.MapProperty(e => e.AssignmentMark).SetElementName("assignmentMark").SetIgnoreIfDefault(false);
                    x.MapProperty(e => e.CourseName).SetElementName("courseName");
                    x.MapProperty(e => e.AttendanceMark).SetElementName("attendanceMark").SetIgnoreIfDefault(false);
                    x.MapProperty(e => e.FinalMark).SetElementName("finalMark").SetIgnoreIfDefault(false);
                    x.MapProperty(e => e.Gpa).SetElementName("gpa");
                    x.MapProperty(e => e.Grade).SetElementName("grade");
                    x.MapProperty(e => e.MidMark).SetElementName("midMark").SetIgnoreIfDefault(false);
                    x.MapProperty(e => e.ProjectMark).SetElementName("projectMark").SetIgnoreIfDefault(false);
                    x.MapProperty(e => e.QuizMark).SetElementName("quizMark").SetIgnoreIfDefault(false);

                });
            }
        }
    }
}

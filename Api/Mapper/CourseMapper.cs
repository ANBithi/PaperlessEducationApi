using Api.Enums;
using Api.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
namespace Api.Mapper
{
    public class CourseMapper
    {
        public static void Map()
        {
            // Mapping should be done once. So, checking if the class has
            // already been registered
            if (!BsonClassMap.IsClassMapRegistered(typeof(Course)))
            {
                BsonClassMap.RegisterClassMap<Course>(x =>
                {
                    x.AutoMap();
                    x.MapProperty(e => e.DepartmentId).SetElementName("departmentId").SetSerializer(new StringSerializer(BsonType.ObjectId));
                    x.MapProperty(e => e.Name).SetElementName("name");
                    x.MapProperty(e => e.Code).SetElementName("code");
                    x.MapProperty(e => e.CourseDetails).SetElementName("courseDetails");
                    x.MapProperty(e => e.Type).SetElementName("type").SetSerializer(new EnumSerializer<CourseTypeEnum>(BsonType.Int32));
                    x.MapProperty(e => e.LessonType).SetElementName("lessonType").SetSerializer(new EnumSerializer<LessonType>(BsonType.Int32));
                    x.MapProperty(e => e.Credits).SetElementName("credits");
                    x.MapProperty(e => e.CourseCover).SetElementName("courseCover");
                    x.MapProperty(e => e.Prerequisites).SetElementName("prerequisites");
                });
            }
        }
    }
}

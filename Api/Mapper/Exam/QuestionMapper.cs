using Api.Enums;
using Api.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Api.Mapper
{
    public class QuestionMapper
    {
        public static void Map()
        {
            // Mapping should be done once. So, checking if the class has
            // already been registered
            if (!BsonClassMap.IsClassMapRegistered(typeof(Question)))
            {
                BsonClassMap.RegisterClassMap<Question>(x =>
                {
                    x.AutoMap();
                    x.MapProperty(e => e.Mark).SetElementName("mark");
                    x.MapProperty(e => e.ExamId).SetElementName("examId").SetSerializer(new StringSerializer(BsonType.ObjectId));
                    x.MapProperty(e => e.Content).SetElementName("content");
                    x.MapProperty(e => e.Options).SetElementName("options");
                    x.MapProperty(e => e.QuestionType).SetElementName("questionType").SetSerializer(new EnumSerializer<QuestionTypeEnum>(BsonType.Int32));


                });
            }
        }
    }
}

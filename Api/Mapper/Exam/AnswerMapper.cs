using Api.Enums;
using Api.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Api.Mapper
{
    public class AnswerMapper
    {
        public static void Map()
        {
            // Mapping should be done once. So, checking if the class has
            // already been registered
            if (!BsonClassMap.IsClassMapRegistered(typeof(Answer)))
            {
                BsonClassMap.RegisterClassMap<Answer>(x =>
                {
                    x.AutoMap();
                    x.MapProperty(e => e.AnswerType).SetElementName("answerType");
                    x.MapProperty(e => e.ObtainedMark).SetElementName("obtainedMark");
                    x.MapProperty(e => e.QuestionId).SetElementName("questionId").SetSerializer(new StringSerializer(BsonType.ObjectId));
                    x.MapProperty(e => e.ExamId).SetElementName("examId").SetSerializer(new StringSerializer(BsonType.ObjectId));
                    x.MapProperty(e => e.Content).SetElementName("content");
                    x.MapProperty(e => e.EvaluationStatus).SetElementName("evaluationStatus").SetSerializer(new EnumSerializer<EvaluationStatus>(BsonType.Int32));
                });
            }
        }
    }
}

using Api.Enums;
using Api.Models.Exam;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Mapper.Exam
{
    public class ExamMetadataMapper
    {
        public static void Map()
        {
            // Mapping should be done once. Check if the class has
            // already been registered
            if (!BsonClassMap.IsClassMapRegistered(typeof(ExamMetadata)))
            {
                BsonClassMap.RegisterClassMap<ExamMetadata>(x =>
                {
                    x.AutoMap();
                    x.MapProperty(e => e.AnswerType).SetElementName("answerType");
                    x.MapProperty(e => e.SectionNumber).SetElementName("sectionNumber");
                    x.MapProperty(e => e.SectionId).SetElementName("sectionId").SetSerializer(new StringSerializer(BsonType.ObjectId));
                    x.MapProperty(e => e.CourseName).SetElementName("courseName");
                    x.MapProperty(e => e.Duration).SetElementName("duration");
                    x.MapProperty(e => e.ExamTitle).SetElementName("examTitle");
                    x.MapProperty(e => e.StartTime).SetElementName("startTime");
                    x.MapProperty(e => e.TotalMarks).SetElementName("totalMarks");
                    x.MapProperty(e => e.CountPercentile).SetElementName("countPercentile").SetSerializer(new EnumSerializer<CountPercentileEnum>(BsonType.Int32));
                    x.MapProperty(e => e.ExamType).SetElementName("examType").SetSerializer(new EnumSerializer<ExamTypeEnum>(BsonType.Int32));

                });
            }
        }
    }
}

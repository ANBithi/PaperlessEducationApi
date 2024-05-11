using Api.Enums;
using Api.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Api.Mapper
{
    public class SectionMapper
    {
        public static void Map()
        {
            // Mapping should be done once. So, checking if the class has
            // already been registered
            if (!BsonClassMap.IsClassMapRegistered(typeof(Section)))
            {
                BsonClassMap.RegisterClassMap<Section>(x =>
                {
                    x.AutoMap();
                    x.MapProperty(e => e.CourseId).SetElementName("courseId").SetSerializer(new StringSerializer(BsonType.ObjectId)); ;
                    x.MapProperty(e => e.SectionNumber).SetElementName("sectionNumber");
                    x.MapProperty(e => e.SectionStartTime).SetElementName("SectionStartTime");
                    x.MapProperty(e => e.SectionEndTime).SetElementName("SectionEndTime");
                    x.MapProperty(e => e.Faculty).SetElementName("faculty").SetSerializer(new StringSerializer(BsonType.ObjectId)); ;
                   
                });
            }
        }


    }

}

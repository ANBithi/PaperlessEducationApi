using Api.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Api.Mapper
{
    public class AcitivityMapper
    {
        public static void Map()
        {
            // Mapping should be done once. So, checking if the class has
            // already been registered
            if (!BsonClassMap.IsClassMapRegistered(typeof(UserActivity)))
            {
                BsonClassMap.RegisterClassMap<UserActivity>(x =>
                {
                    x.AutoMap();
                    x.MapProperty(e => e.ActivityForId).SetElementName("activityForId").SetSerializer(new StringSerializer(BsonType.ObjectId));
                    x.MapProperty(e => e.Status).SetElementName("status").SetSerializer(new EnumSerializer<ActiveStatusEnum>(BsonType.Int32));
                    x.MapProperty(e => e.ActivityFor).SetElementName("activityFor").SetSerializer(new EnumSerializer<ActiveStatuForTypeEnum>(BsonType.Int32));

                });
            }
        }
    }
}

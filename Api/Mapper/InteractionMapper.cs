using Api.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Api.Mapper
{
    public class InteractionMapper
    {
        public static void Map()
        {
            // Mapping should be done once. So, checking if the class has
            // already been registered
            if (!BsonClassMap.IsClassMapRegistered(typeof(UserInteraction)))
            {
                BsonClassMap.RegisterClassMap<UserInteraction>(x =>
                {
                    x.AutoMap();
                    x.MapProperty(e => e.Type).SetElementName("type").SetSerializer(new EnumSerializer<InteractionType>(BsonType.Int32));

                });
            }
        }
    }
}

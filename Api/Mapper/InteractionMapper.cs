using Api.Models.UserInteraction;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Mapper
{
    public class InteractionMapper
    {
        public static void Map()
        {
            // Mapping should be done once. Check if the class has
            // already been registered
            if (!BsonClassMap.IsClassMapRegistered(typeof(UserInteraction)))
            {
                BsonClassMap.RegisterClassMap<UserInteraction>(x =>
                {
                    x.AutoMap();
                    x.MapProperty(e => e.Time).SetElementName("time");
                    x.MapProperty(e => e.Type).SetElementName("type").SetSerializer(new EnumSerializer<InteractionType>(BsonType.Int32));

                });
            }
        }
    }
}

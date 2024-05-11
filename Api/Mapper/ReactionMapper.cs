using Api.Models.Post;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Api.Mapper
{
    public class ReactionMapper
    {
        public static void Map()
        {
            // Mapping should be done once. So, checking if the class has
            // already been registered
            if (!BsonClassMap.IsClassMapRegistered(typeof(Reaction)))
            {
                BsonClassMap.RegisterClassMap<Reaction>(x =>
                {
                    x.AutoMap();                    
                    x.MapProperty(e => e.IconId).SetElementName("iconId");
                    x.MapProperty(e => e.ParentId).SetElementName("parentId").SetSerializer(new StringSerializer(BsonType.ObjectId)); ;

                });
            }
        }
    }
}

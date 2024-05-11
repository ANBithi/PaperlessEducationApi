using Api.Models.Post;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Api.Mapper
{
    public class PostMapper
    {
        public static void Map()
        {
            // Mapping should be done once. So, checking if the class has
            // already been registered
            if (!BsonClassMap.IsClassMapRegistered(typeof(Post)))
            {
                BsonClassMap.RegisterClassMap<Post>(x =>
                {
                    x.AutoMap();
                    x.MapProperty(e => e.CreatorType).SetElementName("creatorType");
                    x.MapProperty(e => e.PostDescription).SetElementName("postDescription");
                    x.MapProperty(e => e.Attachments).SetElementName("attachments");
                    x.MapProperty(e => e.BelongsTo).SetElementName("belongsTo").SetSerializer(new StringSerializer(BsonType.ObjectId)); 

                });
            }
        }
    }
}

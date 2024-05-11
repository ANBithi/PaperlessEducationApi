using Api.Models.UserSpecific;
using MongoDB.Bson.Serialization;

namespace Api.Mapper.UserSpecific
{
    public class PasswordMetadataMapper
    {
        public static void Map()
        {
            // Mapping should be done once. So, checking if the class has
            // already been registered
            if (!BsonClassMap.IsClassMapRegistered(typeof(PasswordMetadata)))
            {
                BsonClassMap.RegisterClassMap<PasswordMetadata>(x =>
                {
               
                    x.MapProperty(e => e.CurrentOtp).SetElementName("currentOtp");

                });
            }
        }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
    public class Notification  : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("dataId")]
        public string DataId { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
    public class Faculty : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("department")]
        public string Department { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("designation")]
        public string Designation { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }
    }
}

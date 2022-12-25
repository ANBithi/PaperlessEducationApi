using Api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Api.Models
{
    public class Contact : AbstractDbEntity
    {        
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("relation")]
        public string Relation { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("mobile")]
        public string Mobile { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

    }
}

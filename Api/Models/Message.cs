using Api.CustomValues;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Api.Models
{
    public class Message : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonElement("creatorType")]
        public int CreatorType { get; set; }

        [BsonElement("messageBody")]
        public string MessageBody { get; set; }

        [BsonElement("atachments")]
        [BsonIgnoreIfDefault(false)]
        public List<Attachment> Attachments { get; set; }

       
    }
}

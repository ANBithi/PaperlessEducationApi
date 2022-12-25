using Api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Api.CustomValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Message : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("createdBy")]
        public string CreatedBy { get; set; }

        [BsonElement("creatorType")]
        public int CreatorType { get; set; }

        [BsonElement("messageBody")]
        public string MessageBody { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("atachments")]
        [BsonIgnoreIfDefault(false)]
        public List<Attachment> Attachments { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("editedAt")]
        public DateTime EditedAt { get; set; }
    }
}

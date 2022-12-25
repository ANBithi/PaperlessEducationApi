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
    public class Post : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("createdBy")]
        public string CreatedBy { get; set; }

        [BsonElement("creatorType")]
        public int CreatorType { get; set; }

        [BsonElement("postDescription")]
        public string PostDescription { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("atachments")]
        public List<Attachment> Attachments { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("editedAt")]
        public DateTime EditedAt { get; set; }

    }
}

using Api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Api.CustomValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Post
{
    public class Post : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        

        [BsonElement("creatorType")]
        public int CreatorType { get; set; }

        [BsonElement("postDescription")]
        public string PostDescription { get; set; }

        

        [BsonElement("atachments")]
        public List<Attachment> Attachments { get; set; }

        

    }
}

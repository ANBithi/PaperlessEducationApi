using Api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Course : AbstractDbEntity
    {

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("department")]
        public string Department { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("code")]
        public string Code { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("sections")]
        public List<string> Sections { get; set; }

        [BsonElement("courseDetails")]
        public string CourseDetails { get; set; }

        [BsonIgnoreIfDefault(false)]
        [BsonElement("type")]
        public CourseTypeEnum Type { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("preRequisities")]
        public List<string> PreRequisities { get; set; }

        [BsonElement("credits")]
        public int Credits { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("addedAt")]
        public DateTime AddedAt { get; set; }

    }
}

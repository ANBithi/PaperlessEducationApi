using Api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Section : AbstractDbEntity
    {

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }
        [BsonElement("sectionNumber")]
        public string SectionNumber { get; set; }

        [BsonElement("maxAllocation")]
        public int MaxAllocation { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("students")]
        public List<string> Students { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("faculty")]
        public string Faculty { get; set; }

        [BsonElement("sectionStartTime")]
        public string SectionStartTime { get; set; }
        [BsonElement("sectionEndTime")]
        public string SectionEndTime { get; set; }

    }
}

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
        public string BelongsTo { get; set; }
        public string Department { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("sections")]
        public List<string> Sections { get; set; }
        public string CourseDetails { get; set; }
        public CourseTypeEnum Type { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("preRequisities")]
        public List<string> PreRequisities { get; set; }
        public int Credits { get; set; }
        public string CourseCover { get; set; }
    }
}

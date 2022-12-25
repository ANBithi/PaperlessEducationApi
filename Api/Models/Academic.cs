using Api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Academic : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonElement("degree")]
        public string Degree { get; set; }

        [BsonElement("examTitle")]
        public string ExamTitle { get; set; }

        [BsonElement("institute")]
        public string Institute { get; set; }

        [BsonElement("boardOrCountry")]
        public string BoardOrCountry { get; set; }

        [BsonElement("majorOrGroup")]
        public string MajorOrGroup { get; set; }

        [BsonElement("result")]
        public string Result { get; set; }

        [BsonElement("cgpaOrMarks")]
        public float CgpaOrMarks { get; set; }

        [BsonElement("scale")]
        public float Scale { get; set; }

        [BsonElement("passedYear")]
        public string PassedYear { get; set; }

        [BsonElement("duration")]
        public string Duration { get; set; }

        [BsonElement("remarks")]
        public string Remarks { get; set; }

        [BsonElement("achievement")]
        public string Achievement { get; set; }

    }
}

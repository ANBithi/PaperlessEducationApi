using Api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Resign : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonElement("isResigning")]
        public bool IsResigning { get; set; }

        [BsonElement("resigned")]
        public bool Resigned { get; set; }

        [BsonElement("reason")]
        public string Reason { get; set; }

        [BsonElement("resignMonth")]
        public string ResignMonth { get; set; }

        [BsonElement("experienceUs")]
        public string ExperienceUs { get; set; }

        [BsonElement("additionalInfo")]
        public string AdditionalInfo { get; set; }

        [BsonElement("achievements")]
        public string Achievements { get; set; }

        [BsonElement("complain")]
        public string Complain { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("supervisor")]
        public string Supervisor { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("refferedBy")]
        public string RefferedBy { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("approvedBy")]
        public string ApprovedBy { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("rejectedBy")]
        public string RejectedBy { get; set; }

    }
}

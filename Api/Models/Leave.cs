using Api.Enums;
using Api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Leave : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonElement("leaveType")]
        public string LeaveType { get; set; }

        [BsonElement("startDate")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime StartDate { get; set; }

        [BsonElement("totalDays")]
        public int TotalDays { get; set; }

        [BsonElement("endDate")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime EndDate { get; set; }

        [BsonElement("isHalfDay")]
        public bool IsHalfDay { get; set; }

        [BsonElement("firstHalf")]
        public bool FirstHalf { get; set; }

        [BsonElement("secondHalf")]
        public bool SecondHalf { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("supervisor")]
        public string Supervisor { get; set; }

        [BsonElement("reason")]
        public string Reason { get; set; }

        [BsonElement("leaveStatus")]
        [BsonIgnoreIfDefault(false)]
        public LeaveStatusEnum LeaveStatus { get; set; }

    }
}

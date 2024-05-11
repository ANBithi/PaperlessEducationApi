using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Api.Models
{
    public class Student : AbstractDbEntity
    {      
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("departmentId")]
        public string DepartmentId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("currentClasses")]
        public List<string> CurrentClasses { get; set; } 

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("advisor")]
        public string Advisor { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("studentID")]
        public string StudentId { get; set; }

        [BsonElement("batch")]
        public string Batch { get; set; }

        [BsonElement("admissionYear")]
        public int AdmissionYear { get; set; }
    }
}

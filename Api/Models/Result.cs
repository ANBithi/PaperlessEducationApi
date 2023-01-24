using Api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{

    public class Result : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonElement("quizMark")]
        [BsonIgnoreIfDefault(false)]
        public float QuizMark { get; set; } 

        [BsonElement("midMark")]
        [BsonIgnoreIfDefault(false)]
        public float MidMark { get; set; } 

        [BsonElement("projectMark")]
        [BsonIgnoreIfDefault(false)]
        public float ProjectMark { get; set; } 

        [BsonElement("attendanceMark")]
        [BsonIgnoreIfDefault(false)]
        public float AttendanceMark { get; set; } 

        [BsonElement("assignmentMark")]
        [BsonIgnoreIfDefault(false)]
        public float AssignmentMark { get; set; } 

        [BsonElement("finalMark")]
        [BsonIgnoreIfDefault(false)]
        public float FinalMark { get; set; }

        [BsonElement("grade")]
        public string Grade { get; set; }

        [BsonElement("gpa")]
        public double Gpa { get; set; }

        [BsonElement("courseName")]
        public string CourseName { get; set; }

        [BsonElement("courseCode")]
        public string CourseCode { get; set; }

    }
}

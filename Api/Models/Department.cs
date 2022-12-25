using Api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Department : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("courses")]
        public List<string> Courses { get; set; }

        [BsonElement("courseDistribution")]
        public string CourseDistribution { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("abbreviation")]
        public string Abbreviation { get; set; }
        [BsonElement("totalCredits")]
        public int TotalCredits { get; set; }
        [BsonElement("minimumCreditPerSem")]
        public int MinimumCreditPerSem { get; set; }

    }
}

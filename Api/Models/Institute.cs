using Api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Institute : AbstractDbEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("departments")]
        public List<string> Departments { get; set; }
        [BsonElement("address")]
        public string Address { get; set; }
        [BsonElement("domain")]
        public string Domain { get; set; }
        [BsonElement("establishedYear")]
        public int EstablishedYear { get; set; }
        [BsonElement("about")]
        public string About { get; set; }
        [BsonElement("semesterDuration")]
        public int SemesterDuration { get; set; }
        [BsonElement("contact ")]
        public string Contact { get; set; }
        [BsonElement("holidays")]
        public List<string> Holidays { get; set; }

    }
}

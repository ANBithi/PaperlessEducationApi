using Api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class EmployeeInfo : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonElement("pin")]
        [BsonIgnoreIfDefault(false)]
        public int Pin { get; set; }

        [BsonElement("salutation")]
        public string Salutation { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("fatherName")]
        public string FatherName { get; set; }

        [BsonElement("motherName")]
        public string MotherName { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("birthDate")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime BirthDate { get; set; }

        [BsonElement("birthPlace")]
        public string BirthPlace { get; set; }

        [BsonElement("bloodGroup")]
        public string BloodGroup { get; set; }

        [BsonElement("nationality")]
        public string Nationality { get; set; }

        [BsonElement("religion")]
        public string Religion { get; set; }

        [BsonElement("maritalStatus")]
        public string MaritalStatus { get; set; }
        [BsonElement("spouseName")]
        public string SpouseName { get; set; }
        [BsonElement("numberSon")]
        [BsonIgnoreIfDefault(false)]
        public int NumberOfSons { get; set; }

        [BsonElement("numberOfDaughters")]
        [BsonIgnoreIfDefault(false)]
        public int NumberOfDaughters { get; set; }

        [BsonElement("cardNo")]
        [BsonIgnoreIfDefault(false)]
        public int CardNo { get; set; }

        [BsonElement("tinNo")]
        [BsonIgnoreIfDefault(false)]
        public Int64 TinNo { get; set; }

        [BsonElement("passportNo")]
        [BsonIgnoreIfDefault(false)]
        public Int64 PassportNo { get; set; }

        [BsonElement("drivingLicense")]
        [BsonIgnoreIfDefault(false)]
        public Int64 DrivingLicense { get; set; }

        [BsonElement("nidNumber")]
        [BsonIgnoreIfDefault(false)]
        public Int64 NidNumber { get; set; }

        [BsonElement("extraCurriculum")]
        public string ExtraCurriculum { get; set; }

        [BsonElement("Remarks")]
        public string Remarks { get; set; }
    }
}

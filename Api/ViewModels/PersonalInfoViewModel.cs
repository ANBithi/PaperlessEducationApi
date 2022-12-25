using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels
{
    public class PersonalInfoViewModel
    {

        public string BelongsTo { get; set; }
        public int Pin { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string BloodGroup { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string MaritalStatus { get; set; }
        public string SpouseName { get; set; }
        public int NumberOfSons { get; set; }
        public int NumberOfDaughters { get; set; }
        public int CardNo { get; set; }
        public Int64 TinNo { get; set; }
        public Int64 PassportNo { get; set; }
        public Int64 DrivingLicense { get; set; }
        public Int64 NidNumber { get; set; }
        public string ExtraCurriculum { get; set; }
        public string Remarks { get; set; }
    }



    public class AddInfoRequest
    {
        public string BelongsTo { get; set; }
        public int Pin { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string BloodGroup { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string MaritalStatus { get; set; }
        public string SpouseName { get; set; }
        public int NumberOfSons { get; set; }
        public int NumberOfDaughters { get; set; }
        public int CardNo { get; set; }
        public Int64 TinNo { get; set; }
        public Int64 PassportNo { get; set; }
        public Int64 DrivingLicense { get; set; }
        public Int64 NidNumber { get; set; }
        public string ExtraCurriculum { get; set; }
        public string Remarks { get; set; }
    }
}

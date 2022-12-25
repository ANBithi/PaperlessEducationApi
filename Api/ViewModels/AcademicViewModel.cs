using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels
{
    public class AcademicViewModel
    {
        public string Id { get; set; }
        public string Degree { get; set; }
        public string ExamTitle { get; set; }
        public string Institute { get; set; }
        public string BoardOrCountry { get; set; }
        public string MajorOrGroup { get; set; }
        public string Result { get; set; }
        public float CgpaOrMarks { get; set; }
        public float Scale { get; set; }
        public string PassedYear { get; set; }
        public string Duration { get; set; }
        public string Remarks { get; set; }
        public string Achievement { get; set; }
    }


    public class AddAcademicRequest
    {
        public string BelongsTo { get; set; }
        public string Degree { get; set; }
        public string ExamTitle { get; set; }
        public string Institute { get; set; }
        public string BoardOrCountry { get; set; }
        public string MajorOrGroup { get; set; }
        public string Result { get; set; }
        public float CgpaOrMarks { get; set; }
        public float Scale { get; set; }
        public string PassedYear { get; set; }
        public string Duration { get; set; }
        public string Remarks { get; set; }
        public string Achievement { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels
{
    public class ProfQualificationViewModel
    {
        public string Id { get; set; }
        public string CourseType { get; set; }
        public string CourseTitle { get; set; }
        public string Institute { get; set; }
        public string Address { get; set; }
        public string Duration { get; set; }
        public string Result { get; set; }
        public string Year { get; set; }
        public string Achievement { get; set; }
    }


    public class AddProfRequest
    {
        public string BelongsTo { get; set; }
        public string CourseType { get; set; }
        public string CourseTitle { get; set; }
        public string Institute { get; set; }
        public string Address { get; set; }
        public string Duration { get; set; }
        public string Result { get; set; }
        public string Year { get; set; }
        public string Achievement { get; set; }
    }
}

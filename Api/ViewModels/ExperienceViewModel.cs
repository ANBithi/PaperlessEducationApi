using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels
{
    public class ExperienceViewModel
    {
        public string Id { get; set; }
        public string Company { get; set; }
        public string Business { get; set; }
        public string Location { get; set; }
        public string PositionHeld { get; set; }
        public string JobNature { get; set; }
        public string Responsibilities { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Department { get; set; }
        public string LastSalary { get; set; }
        public string Remarks { get; set; }
    }



    public class AddExperienceRequest
    {
        public string BelongsTo { get; set; }
        public string Company { get; set; }
        public string Business { get; set; }
        public string Location { get; set; }
        public string PositionHeld { get; set; }
        public string JobNature { get; set; }
        public string Responsibilities { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Department { get; set; }
        public string LastSalary { get; set; }
        public string Remarks { get; set; }
    }
}

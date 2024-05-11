using Api.Enums;
using System;
using System.Collections.Generic;

namespace Api.Models
{
    public class Section : AbstractDbEntity
    {

       
        public string CourseId { get; set; }

        public string SectionNumber { get; set; }

        public int MaxAllocation { get; set; }

        public List<string> Students { get; set; }

        public string Faculty { get; set; }

        public string SectionStartTime { get; set; }
        public string SectionEndTime { get; set; }

    }
}

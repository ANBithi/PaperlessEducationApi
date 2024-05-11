using System;

namespace Api.Requests.SectionRequests
{
    public class AddSectionRequest
    {
        public string BelongsTo { get; set; }
        public string SectionNumber { get; set; }
        public int MaxAllocation { get; set; }
        public string Faculty { get; set; }
        public string SectionStartTime { get; set; }
        public string SectionEndTime { get; set; }
    }
}

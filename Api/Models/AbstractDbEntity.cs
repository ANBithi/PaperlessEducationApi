using System;

namespace Api.Models
{
    public abstract class AbstractDbEntity
    {
        public string Id { get; set; }
        public string CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedById { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}

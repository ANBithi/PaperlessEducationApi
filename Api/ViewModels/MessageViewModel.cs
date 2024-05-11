using Api.CustomValues;
using System;
using System.Collections.Generic;

namespace Api.ViewModels
{
    public class MessageViewModel
    {
        public string CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public string MessageBody { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}

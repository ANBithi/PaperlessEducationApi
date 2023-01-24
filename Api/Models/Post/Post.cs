using Api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Api.CustomValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Post
{
    public class Post : AbstractDbEntity
    {
        public string BelongsTo { get; set; }
        public int CreatorType { get; set; }
        public string PostDescription { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}

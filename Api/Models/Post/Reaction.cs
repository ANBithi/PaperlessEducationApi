using Api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Post
{
    public class Reaction : AbstractDbEntity
    {        
        public string ParentId { get; set; }
        public string IconId { get; set; }
    }
}

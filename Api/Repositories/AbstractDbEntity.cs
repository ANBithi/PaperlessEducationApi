using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public abstract class AbstractDbEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]        
        public string Id { get; set; }
    }
}

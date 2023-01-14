using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;

namespace Api.Mapper
{
    public class AbstractDbEntityMapper
    {
        public static void Map()
        {
            // Mapping should be done once. Check if the class has
            // already been registered
            if (!BsonClassMap.IsClassMapRegistered(typeof(AbstractDbEntity)))
            {
                BsonClassMap.RegisterClassMap<AbstractDbEntity>(x =>
                {
                    x.AutoMap();
                    x.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance)
                        .SetSerializer(new StringSerializer(BsonType.ObjectId));
                    x.MapProperty(e => e.CreatedAt).SetElementName("createdAt");
                    x.MapProperty(e => e.CreatedById).SetElementName("createdById").SetSerializer(new StringSerializer(BsonType.ObjectId)); ;
                    x.MapProperty(e => e.CreatedBy).SetElementName("createdBy");
                    x.MapProperty(e => e.ModifiedAt).SetElementName("modifiedAt");
                    x.MapProperty(e => e.ModifiedById).SetElementName("modifiedById").SetSerializer(new StringSerializer(BsonType.ObjectId)); ;
                    x.MapProperty(e => e.ModifiedBy).SetElementName("modifiedBy");

                });
            }
        }
       

    }


}

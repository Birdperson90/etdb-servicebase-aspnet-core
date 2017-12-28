using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Etdb.ServiceBase.Domain.Abstractions.Base
{
    public abstract class Entity : IEntity
    {
        [BsonId]
        public string Id { get; set; }
        public byte[] RowVersion { get; set; }
    }
}

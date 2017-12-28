using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Events
{
    public abstract class Event : Message
    {
        [BsonElement]
        public DateTime Timestamp { get; }

        protected Event()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}

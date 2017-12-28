using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(Message theEvent, string user)
        {
            Id = Guid.NewGuid().ToString();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = theEvent;
            User = user;
        }

        [BsonId]
        public string Id { get; }

        [BsonElement]
        public Message Data { get; }

        [BsonElement]
        public string User { get; }
    }
}

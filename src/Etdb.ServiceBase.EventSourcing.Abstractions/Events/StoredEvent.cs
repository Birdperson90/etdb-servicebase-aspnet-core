using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(Message theEvent, string data, string user)
        {
            Id = Guid.NewGuid().ToString();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

        [BsonId]
        public string Id { get; }

        public string Data { get; }

        public string User { get; }
    }
}

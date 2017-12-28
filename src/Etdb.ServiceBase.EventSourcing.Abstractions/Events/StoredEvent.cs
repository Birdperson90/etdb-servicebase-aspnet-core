using System;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(Message theEvent, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

        public Guid Id { get; }

        public string Data { get; }

        public string User { get; }
    }
}

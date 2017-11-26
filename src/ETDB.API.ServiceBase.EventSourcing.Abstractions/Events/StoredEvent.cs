using System;

namespace ETDB.API.ServiceBase.EventSourcing.Abstractions.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(Event theEvent, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

        // EF Core needs this
        // TODO: fix this at some point
        public StoredEvent() { }

        public Guid Id { get; private set; }

        public string Data { get; private set; }

        public string User { get; private set; }
    }
}

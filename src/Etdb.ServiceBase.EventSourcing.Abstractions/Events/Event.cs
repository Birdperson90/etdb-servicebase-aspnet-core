using System;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Events
{
    public abstract class Event : Message
    {
        public DateTime Timestamp { get; }

        protected Event()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}

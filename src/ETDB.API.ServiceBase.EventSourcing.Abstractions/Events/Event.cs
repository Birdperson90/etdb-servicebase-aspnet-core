using System;

namespace ETDB.API.ServiceBase.EventSourcing.Abstractions.Events
{
    public abstract class Event : Message
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            // TODO MAKE THIS UTC
            Timestamp = DateTime.Now;
        }
    }
}

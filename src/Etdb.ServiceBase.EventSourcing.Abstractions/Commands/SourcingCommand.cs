using System;
using Etdb.ServiceBase.EventSourcing.Abstractions.Events;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Commands
{
    public abstract class SourcingCommand : Message
    {
        public DateTime Timestamp { get; }

        protected SourcingCommand()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}

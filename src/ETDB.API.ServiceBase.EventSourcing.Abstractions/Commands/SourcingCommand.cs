using System;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Events;
using FluentValidation.Results;

namespace ETDB.API.ServiceBase.EventSourcing.Abstractions.Commands
{
    public abstract class SourcingCommand : Message
    {
        public DateTime Timestamp { get; }
        public ValidationResult ValidationResult { get; set; }

        protected SourcingCommand()
        {
            Timestamp = DateTime.UtcNow;
        }

        public abstract bool IsValid();
    }
}

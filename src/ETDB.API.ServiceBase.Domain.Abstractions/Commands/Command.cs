using System;
using ETDB.API.ServiceBase.Domain.Abstractions.Events;
using FluentValidation.Results;

namespace ETDB.API.ServiceBase.Domain.Abstractions.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.UtcNow;
        }

        public abstract bool IsValid();
    }
}

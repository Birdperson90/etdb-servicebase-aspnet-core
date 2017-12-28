using System;
using Etdb.ServiceBase.EventSourcing.Abstractions.Events;
using MediatR;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Commands
{
    public abstract class TransactionCommand<TResponse> : Message, IRequest<TResponse> where TResponse : class
    {
        public DateTime Timestamp { get; }

        protected TransactionCommand()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}

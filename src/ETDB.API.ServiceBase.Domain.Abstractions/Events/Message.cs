using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace ETDB.API.ServiceBase.Domain.Abstractions.Events
{
    public abstract class Message : INotification
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}

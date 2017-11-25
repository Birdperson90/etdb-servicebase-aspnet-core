using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace ETDB.API.ServiceBase.Domain.Abstractions.Events
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : Message
    {
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ETDB.API.ServiceBase.Domain.Abstractions.Events;

namespace ETDB.API.ServiceBase.Abstractions.Handler
{
    public abstract class DomainEventHandler<TEvent> : IDomainEventHandler<TEvent> where TEvent : Event
    {
        public abstract void Handle(TEvent notification);
    }
}

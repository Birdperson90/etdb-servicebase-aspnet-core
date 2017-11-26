using ETDB.API.ServiceBase.EventSourcing.Abstractions.Events;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Handler;

namespace ETDB.API.ServiceBase.EventSourcing.Handler
{
    public abstract class DomainEventHandler<TEvent> : IDomainEventHandler<TEvent> where TEvent : Event
    {
        public abstract void Handle(TEvent notification);
    }
}

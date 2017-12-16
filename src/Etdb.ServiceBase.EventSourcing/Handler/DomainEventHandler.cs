using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.EventSourcing.Abstractions.Events;
using Etdb.ServiceBase.EventSourcing.Abstractions.Handler;

namespace Etdb.ServiceBase.EventSourcing.Handler
{
    public abstract class DomainEventHandler<TEvent> : IDomainEventHandler<TEvent> where TEvent : Event
    {
        public abstract void Handle(TEvent notification);
        public abstract Task Handle(TEvent notification, CancellationToken cancellationToken);
    }
}

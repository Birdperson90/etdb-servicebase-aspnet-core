using Etdb.ServiceBase.EventSourcing.Abstractions.Events;
using MediatR;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Handler
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : Message
    {
        
    }
}

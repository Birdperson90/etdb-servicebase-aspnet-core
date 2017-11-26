using ETDB.API.ServiceBase.EventSourcing.Abstractions.Events;
using MediatR;

namespace ETDB.API.ServiceBase.EventSourcing.Abstractions.Handler
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : Message
    {
        
    }
}

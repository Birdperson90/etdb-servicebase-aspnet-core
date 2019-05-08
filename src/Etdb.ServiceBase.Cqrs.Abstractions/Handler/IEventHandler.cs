using Etdb.ServiceBase.Cqrs.Abstractions.Messages;
using MediatR;

namespace Etdb.ServiceBase.Cqrs.Abstractions.Handler
{
    public interface IEventHandler<in TMessage> : INotificationHandler<TMessage> where TMessage : IEvent
    {
    }
}
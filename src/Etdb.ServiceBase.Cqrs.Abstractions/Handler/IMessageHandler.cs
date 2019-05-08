using Etdb.ServiceBase.Cqrs.Abstractions.Messages;
using MediatR;

namespace Etdb.ServiceBase.Cqrs.Abstractions.Handler
{
    public interface IMessageHandler<in TMessage> : INotificationHandler<TMessage> where TMessage : IEvent
    {
    }
}
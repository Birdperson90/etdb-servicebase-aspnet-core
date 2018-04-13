using Etdb.ServiceBase.Cqrs.Abstractions.Notifications;
using MediatR;

namespace Etdb.ServiceBase.Cqrs.Abstractions.Handler
{
    public interface INotifierHandler<in TNotifier> : INotificationHandler<TNotifier> where TNotifier : INotifier
    {
        
    }
}
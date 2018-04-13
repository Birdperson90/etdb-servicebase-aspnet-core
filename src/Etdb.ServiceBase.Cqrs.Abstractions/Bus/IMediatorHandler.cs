using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using Etdb.ServiceBase.Cqrs.Abstractions.Notifications;

namespace Etdb.ServiceBase.Cqrs.Abstractions.Bus
{
    public interface IMediatorHandler
    {
        Task<TResponse> SendCommandAsync<TResponseCommand, TResponse>(TResponseCommand command)
            where TResponseCommand : IResponseCommand<TResponse>;

        Task SendCommandAsync<TVoidCommand>(TVoidCommand command) where TVoidCommand : IVoidCommand;

        Task NotifyAsync<TNotifier>(TNotifier notifier) where TNotifier : INotifier;
        
        Task<TResponse> SendCommand<TResponseCommand, TResponse>(TResponseCommand command)
            where TResponseCommand : IResponseCommand<TResponse>;
        
        Task SendCommand<TVoidCommand>(TVoidCommand command) where TVoidCommand : IVoidCommand;
        
        Task Notify<TNotifier>(TNotifier notifier) where TNotifier : INotifier;
    }
}
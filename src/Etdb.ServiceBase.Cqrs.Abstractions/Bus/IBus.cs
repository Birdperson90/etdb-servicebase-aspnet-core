using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using Etdb.ServiceBase.Cqrs.Abstractions.Messages;

namespace Etdb.ServiceBase.Cqrs.Abstractions.Bus
{
    public interface IBus
    {
        Task<TResponse> SendCommandAsync<TResponseCommand, TResponse>(TResponseCommand command,
            CancellationToken ctx = default)
            where TResponseCommand : IResponseCommand<TResponse>;

        Task SendCommandAsync<TVoidCommand>(TVoidCommand command, CancellationToken ctx = default)
            where TVoidCommand : IVoidCommand;

        Task NotifyAsync<TNotifier>(TNotifier notifier, CancellationToken ctx = default) where TNotifier : IMessage;

        Task<TResponse> SendCommand<TResponseCommand, TResponse>(TResponseCommand command,
            CancellationToken ctx = default)
            where TResponseCommand : IResponseCommand<TResponse>;

        Task SendCommand<TVoidCommand>(TVoidCommand command, CancellationToken ctx = default)
            where TVoidCommand : IVoidCommand;

        Task Notify<TNotifier>(TNotifier notifier, CancellationToken ctx = default) where TNotifier : IMessage;
    }
}
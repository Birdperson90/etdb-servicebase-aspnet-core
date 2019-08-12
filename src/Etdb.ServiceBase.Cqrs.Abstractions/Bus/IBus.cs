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
            where TResponseCommand : IResponseCommand<TResponse> where TResponse : class;

        Task SendCommandAsync<TVoidCommand>(TVoidCommand command, CancellationToken ctx = default)
            where TVoidCommand : IVoidCommand;

        Task RaiseEventAsync<TEvent>(TEvent @event, CancellationToken ctx = default) where TEvent : IEvent;
    }
}
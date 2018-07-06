using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Bus;
using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using Etdb.ServiceBase.Cqrs.Abstractions.Messages;
using MediatR;

namespace Etdb.ServiceBase.Cqrs.Bus
{
    public class Bus : IBus
    {
        private readonly IMediator mediator;

        public Bus(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<TResponse> SendCommandAsync<TResponseCommand, TResponse>(TResponseCommand command,
            CancellationToken ctx = default)
            where TResponseCommand : IResponseCommand<TResponse>
        {
            return await this.mediator.Send(command, ctx).ConfigureAwait(false);
        }

        public async Task SendCommandAsync<TVoidCommand>(TVoidCommand command, CancellationToken ctx = default)
            where TVoidCommand : IVoidCommand
        {
            await this.mediator.Send(command, ctx).ConfigureAwait(false);
        }

        public async Task NotifyAsync<TMessage>(TMessage notifier, CancellationToken ctx = default)
            where TMessage : IMessage
        {
            await this.mediator.Publish(notifier, ctx).ConfigureAwait(false);
        }

        public Task<TResponse> SendCommand<TResponseCommand, TResponse>(TResponseCommand command,
            CancellationToken ctx = default)
            where TResponseCommand : IResponseCommand<TResponse>
        {
            return this.mediator.Send(command, ctx);
        }

        public Task SendCommand<TVoidCommand>(TVoidCommand command, CancellationToken ctx = default)
            where TVoidCommand : IVoidCommand
        {
            return this.mediator.Send(command, ctx);
        }

        public Task Notify<TMessage>(TMessage notifier, CancellationToken ctx = default) where TMessage : IMessage
        {
            return this.mediator.Publish(notifier, ctx);
        }
    }
}
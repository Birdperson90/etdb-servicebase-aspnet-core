using System;
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

        public async Task<TResponse?> SendCommandAsync<TResponseCommand, TResponse>(TResponseCommand command,
            CancellationToken ctx = default)
            where TResponseCommand : IResponseCommand<TResponse?> where TResponse : class
        {
            return await this.mediator.Send(command, ctx).ConfigureAwait(false);
        }

        public async Task SendCommandAsync<TVoidCommand>(TVoidCommand command, CancellationToken ctx = default)
            where TVoidCommand : IVoidCommand
        {
            await this.mediator.Send(command, ctx).ConfigureAwait(false);
        }

        public async Task RaiseEventAsync<TMessage>(TMessage @event, CancellationToken ctx = default)
            where TMessage : IEvent
        {
            await this.mediator.Publish(@event, ctx).ConfigureAwait(false);
        }
    }
}
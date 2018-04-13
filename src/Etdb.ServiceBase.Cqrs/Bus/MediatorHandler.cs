using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Bus;
using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using Etdb.ServiceBase.Cqrs.Abstractions.Notifications;
using MediatR;

namespace Etdb.ServiceBase.Cqrs.Bus
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator mediator;

        public MediatorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        public async Task<TResponse> SendCommandAsync<TResponseCommand, TResponse>(TResponseCommand command) 
            where TResponseCommand : IResponseCommand<TResponse>
        {
            return await this.mediator.Send(command).ConfigureAwait(false);
        }

        public async Task SendCommandAsync<TVoidCommand>(TVoidCommand command) 
            where TVoidCommand : IVoidCommand
        {
            await this.mediator.Send(command).ConfigureAwait(false);
        }

        public async Task NotifyAsync<TNotifier>(TNotifier notifier) 
            where TNotifier : INotifier
        {
            await this.mediator.Publish(notifier).ConfigureAwait(false);
        }

        public Task<TResponse> SendCommand<TResponseCommand, TResponse>(TResponseCommand command) where TResponseCommand : IResponseCommand<TResponse>
        {
            return this.mediator.Send(command);
        }

        public Task SendCommand<TVoidCommand>(TVoidCommand command) where TVoidCommand : IVoidCommand
        {
            return this.mediator.Send(command);
        }

        public Task Notify<TNotifier>(TNotifier notifier) where TNotifier : INotifier
        {
            return this.mediator.Publish(notifier);
        }
    }
}
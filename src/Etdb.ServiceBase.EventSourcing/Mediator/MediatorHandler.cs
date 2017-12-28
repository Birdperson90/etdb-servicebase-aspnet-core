using System.Threading.Tasks;
using Etdb.ServiceBase.EventSourcing.Abstractions.Bus;
using Etdb.ServiceBase.EventSourcing.Abstractions.Commands;
using Etdb.ServiceBase.EventSourcing.Abstractions.Events;
using Etdb.ServiceBase.EventSourcing.Abstractions.Notifications;
using Etdb.ServiceBase.EventSourcing.Abstractions.Repositories;
using MediatR;

namespace Etdb.ServiceBase.EventSourcing.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator mediator;
        private readonly IEventStore eventStore;

        public MediatorHandler(IEventStore eventStore, IMediator mediator)
        {
            this.eventStore = eventStore;
            this.mediator = mediator;
        }

        public Task<TResponse> SendCommand<TTransactionCommand, TResponse>(TTransactionCommand command)
            where TTransactionCommand : TransactionCommand<TResponse>
            where TResponse : class
        {
            return this.mediator.Send(command);
        }

        public async Task<TResponse> SendCommandAsync<TTransactionCommand, TResponse>(TTransactionCommand command) 
            where TTransactionCommand : TransactionCommand<TResponse>
            where TResponse : class
        {
            return await this.mediator.Send(command);
        }

        public Task RaiseEvent<TEvent>(TEvent @event) where TEvent : Event
        {
            if (!typeof(DomainNotification).IsAssignableFrom(@event.Type))
            {
                this.eventStore.Save(@event);
            }

            return this.Publish(@event);
        }

        private Task Publish<TMessage>(TMessage message) where TMessage : Message
        {
            return mediator.Publish(message);
        }
    }
}

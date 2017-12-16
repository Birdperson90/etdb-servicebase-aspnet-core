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

        public Task SendCommand<T>(T command) where T : SourcingCommand
        {
            return this.Publish(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            if (!@event.Type.IsAssignableFrom(typeof(DomainNotification)))
            {
                this.eventStore.Save(@event);
            }

            return this.Publish(@event);
        }

        private Task Publish<T>(T message) where T : Message
        {
            return mediator.Publish(message);
        }
    }
}

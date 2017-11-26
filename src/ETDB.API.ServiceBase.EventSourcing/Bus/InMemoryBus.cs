using System.Threading.Tasks;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Bus;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Commands;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Events;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Notifications;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Repositories;
using MediatR;

namespace ETDB.API.ServiceBase.EventSourcing.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator mediator;
        private readonly IEventStore eventStore;
        //private const string DomainNotification = "DomainNotification";

        public InMemoryBus(IEventStore eventStore, IMediator mediator)
        {
            this.eventStore = eventStore;
            this.mediator = mediator;
        }

        public Task SendCommand<T>(T command) where T : SourcingCommand
        {
            return Publish(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            //if (!@event.MessageType.Equals(nameof(DomainNotification)))
            //{
            //    eventStore.Save(@event);
            //}

            if (!@event.Type.IsAssignableFrom(typeof(DomainNotification)))
            {
                this.eventStore.Save(@event);
            }

            return Publish(@event);
        }

        private Task Publish<T>(T message) where T : Message
        {
            return mediator.Publish(message);
        }
    }
}

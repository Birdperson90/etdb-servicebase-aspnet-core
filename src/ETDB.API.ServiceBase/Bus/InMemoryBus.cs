using System;
using System.Threading.Tasks;
using ETDB.API.ServiceBase.Abstractions.EventSourcing;
using ETDB.API.ServiceBase.Domain.Abstractions.Bus;
using ETDB.API.ServiceBase.Domain.Abstractions.Commands;
using ETDB.API.ServiceBase.Domain.Abstractions.Events;
using MediatR;

namespace ETDB.API.ServiceBase.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator mediator;
        private readonly IEventStore eventStore;
        private const string DomainNotification = "DomainNotification";

        public InMemoryBus(IEventStore eventStore, IMediator mediator)
        {
            this.eventStore = eventStore;
            this.mediator = mediator;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return Publish(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            if (!@event.MessageType.Equals(InMemoryBus.DomainNotification))
            {
                eventStore?.Save(@event);
            }

            return Publish(@event);
        }

        private Task Publish<T>(T message) where T : Message
        {
            return mediator.Publish(message);
        }
    }
}

using System.Threading.Tasks;
using Etdb.ServiceBase.EventSourcing.Abstractions.Base;
using Etdb.ServiceBase.EventSourcing.Abstractions.Events;
using Etdb.ServiceBase.EventSourcing.Abstractions.Repositories;
using Newtonsoft.Json;

namespace Etdb.ServiceBase.EventSourcing.Repositories
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository eventStoreRepository;
        private readonly IEventUser eventUser;

        public EventStore(IEventStoreRepository eventStoreRepository, IEventUser eventUser)
        {
            this.eventStoreRepository = eventStoreRepository;
            this.eventUser = eventUser;
        }

        public async Task Save<TEvent>(TEvent @event) where TEvent : Event
        {
            var storedEvent = new StoredEvent(@event, this.eventUser.UserName);

            await this.eventStoreRepository.Store(storedEvent);
        }
    }
}

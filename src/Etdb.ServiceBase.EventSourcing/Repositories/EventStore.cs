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

        public void Save<TEvent>(TEvent @event) where TEvent : Event
        {
            var serializedData = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            var storedEvent = new StoredEvent(
                @event,
                serializedData,
                this.eventUser.UserName);

            this.eventStoreRepository.Store(storedEvent);
        }
    }
}

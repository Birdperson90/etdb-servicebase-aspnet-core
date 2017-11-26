using ETDB.API.ServiceBase.EventSourcing.Abstractions.Base;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Events;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Repositories;
using Newtonsoft.Json;

namespace ETDB.API.ServiceBase.EventSourcing.Repositories
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

        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(theEvent, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                this.eventUser.UserName);

            this.eventStoreRepository.Store(storedEvent);
        }
    }
}

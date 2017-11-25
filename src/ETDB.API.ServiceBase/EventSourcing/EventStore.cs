using ETDB.API.ServiceBase.Abstractions.EventSourcing;
using ETDB.API.ServiceBase.Abstractions.Repositories;
using ETDB.API.ServiceBase.Domain.Abstractions.Base;
using ETDB.API.ServiceBase.Domain.Abstractions.Events;
using Newtonsoft.Json;

namespace ETDB.API.ServiceBase.EventSourcing
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
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                this.eventUser.UserName);

            this.eventStoreRepository.Store(storedEvent);
        }
    }
}

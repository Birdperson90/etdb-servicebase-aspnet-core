using System;
using System.Collections.Generic;
using System.Linq;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Events;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Repositories;
using ETDB.API.ServiceBase.EventSourcing.ContextBase;

namespace ETDB.API.ServiceBase.EventSourcing.Repositories
{
    public class EventStoreRepository : IEventStoreRepository, IDisposable
    {
        private readonly EventStoreContextBase context;

        public EventStoreRepository(EventStoreContextBase context)
        {
            this.context = context;
        }

        public IList<StoredEvent> All(Guid aggregateId)
        {
            return this.context.Set<StoredEvent>()
                .AsQueryable()
                .Where(storedEvent => storedEvent.AggregateId == aggregateId)
                .ToList();
        }

        public void Store(StoredEvent theEvent)
        {
            context.Set<StoredEvent>().Add(theEvent);
            context.SaveChanges();
        }

        public void Dispose()
        {
            this.context?.Dispose();
        }
    }
}

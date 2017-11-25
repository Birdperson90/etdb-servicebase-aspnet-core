using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETDB.API.ServiceBase.Abstractions.Repositories;
using ETDB.API.ServiceBase.ContextBase;
using ETDB.API.ServiceBase.Domain.Abstractions.Events;
using Microsoft.EntityFrameworkCore;

namespace ETDB.API.ServiceBase.Repositories
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

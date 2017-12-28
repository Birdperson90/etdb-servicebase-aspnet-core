using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etdb.ServiceBase.EventSourcing.Abstractions.Base;
using Etdb.ServiceBase.EventSourcing.Abstractions.Events;
using Etdb.ServiceBase.EventSourcing.Abstractions.Repositories;
using MongoDB.Driver;

namespace Etdb.ServiceBase.EventSourcing.Repositories
{
    public class EventStoreRepository : IEventStoreRepository, IDisposable
    {
        private EventStoreContextBase context;

        public EventStoreRepository(EventStoreContextBase context)
        {
            this.context = context;
        }

        public IList<StoredEvent> All(Guid aggregateId)
        {
            return this.context.Database.GetCollection<StoredEvent>("StoredEvents")
                .AsQueryable()
                .Where(storedEvent => storedEvent.AggregateId == aggregateId)
                .ToList();
        }

        public async Task Store(StoredEvent theEvent)
        {
            await this.context
                .Database
                .GetCollection<StoredEvent>("StoredEvents")
                .InsertOneAsync(theEvent);
        }

        public void Dispose()
        {
            this.context = null;
        }
    }
}

using System;
using System.Collections.Generic;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Events;

namespace ETDB.API.ServiceBase.EventSourcing.Abstractions.Repositories
{
    public interface IEventStoreRepository
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}

using System;
using System.Collections.Generic;
using Etdb.ServiceBase.EventSourcing.Abstractions.Events;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Repositories
{
    public interface IEventStoreRepository
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}

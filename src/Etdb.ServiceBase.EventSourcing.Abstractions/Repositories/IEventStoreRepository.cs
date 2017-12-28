using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Etdb.ServiceBase.EventSourcing.Abstractions.Events;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Repositories
{
    public interface IEventStoreRepository
    {
        Task Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}

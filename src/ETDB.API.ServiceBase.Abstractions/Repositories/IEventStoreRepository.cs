using System;
using System.Collections.Generic;
using System.Text;
using ETDB.API.ServiceBase.Domain.Abstractions.Events;

namespace ETDB.API.ServiceBase.Abstractions.Repositories
{
    public interface IEventStoreRepository
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}

using MongoDB.Driver;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Base
{
    public abstract class EventStoreContextBase
    {
        public abstract IMongoDatabase Database { get; }
    }
}

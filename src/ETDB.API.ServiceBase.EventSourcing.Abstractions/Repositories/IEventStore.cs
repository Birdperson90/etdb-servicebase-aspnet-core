using ETDB.API.ServiceBase.EventSourcing.Abstractions.Events;

namespace ETDB.API.ServiceBase.EventSourcing.Abstractions.Repositories
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}

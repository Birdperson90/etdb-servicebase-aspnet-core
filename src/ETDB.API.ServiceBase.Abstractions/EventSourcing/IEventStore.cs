using ETDB.API.ServiceBase.Domain.Abstractions.Events;

namespace ETDB.API.ServiceBase.Abstractions.EventSourcing
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}

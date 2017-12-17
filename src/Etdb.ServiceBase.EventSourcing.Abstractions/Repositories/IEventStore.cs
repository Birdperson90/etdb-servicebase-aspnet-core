using Etdb.ServiceBase.EventSourcing.Abstractions.Events;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Repositories
{
    public interface IEventStore
    {
        void Save<T>(T @event) where T : Event;
    }
}

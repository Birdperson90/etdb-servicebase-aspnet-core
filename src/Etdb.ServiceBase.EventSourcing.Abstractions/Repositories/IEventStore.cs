using System.Threading.Tasks;
using Etdb.ServiceBase.EventSourcing.Abstractions.Events;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Repositories
{
    public interface IEventStore
    {
        Task Save<T>(T @event) where T : Event;
    }
}

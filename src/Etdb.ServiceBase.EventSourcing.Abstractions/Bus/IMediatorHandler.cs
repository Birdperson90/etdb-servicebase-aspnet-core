using System.Threading.Tasks;
using Etdb.ServiceBase.EventSourcing.Abstractions.Commands;
using Etdb.ServiceBase.EventSourcing.Abstractions.Events;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : SourcingCommand;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}

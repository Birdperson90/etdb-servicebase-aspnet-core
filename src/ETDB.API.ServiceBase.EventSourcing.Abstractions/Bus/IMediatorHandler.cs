using System.Threading.Tasks;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Commands;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Events;

namespace ETDB.API.ServiceBase.EventSourcing.Abstractions.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : SourcingCommand;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}

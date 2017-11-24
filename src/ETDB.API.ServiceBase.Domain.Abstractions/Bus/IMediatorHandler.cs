using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETDB.API.ServiceBase.Domain.Abstractions.Commands;
using ETDB.API.ServiceBase.Domain.Abstractions.Events;

namespace ETDB.API.ServiceBase.Domain.Abstractions.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}

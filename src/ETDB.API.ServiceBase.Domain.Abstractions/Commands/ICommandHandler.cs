using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace ETDB.API.ServiceBase.Domain.Abstractions.Commands
{
    public interface ICommandHandler<in TCommand>  : INotificationHandler<TCommand> where TCommand : Command
    {
        bool Commit();
        void NotifyValidationErrors(TCommand message);
    }
}

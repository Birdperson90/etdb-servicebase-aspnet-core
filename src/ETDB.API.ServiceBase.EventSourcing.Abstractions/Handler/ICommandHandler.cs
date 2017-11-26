using ETDB.API.ServiceBase.EventSourcing.Abstractions.Commands;
using FluentValidation.Results;
using MediatR;

namespace ETDB.API.ServiceBase.EventSourcing.Abstractions.Handler
{
    public interface ICommandHandler<in TCommand>  : INotificationHandler<TCommand> where TCommand : SourcingCommand
    {
        bool Commit();
        void NotifyValidationErrors(TCommand message, ValidationResult validationResult);
    }
}

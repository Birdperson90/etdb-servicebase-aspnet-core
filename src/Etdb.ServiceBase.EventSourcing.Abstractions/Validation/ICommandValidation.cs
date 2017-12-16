using Etdb.ServiceBase.EventSourcing.Abstractions.Commands;
using FluentValidation.Results;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Validation
{
    public interface ICommandValidation<in TCommand> where TCommand : SourcingCommand
    {
        bool IsValid(TCommand sourcingCommand, out ValidationResult validationResult);
    }
}

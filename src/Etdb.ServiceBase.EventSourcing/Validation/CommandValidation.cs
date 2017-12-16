using Etdb.ServiceBase.EventSourcing.Abstractions.Commands;
using Etdb.ServiceBase.EventSourcing.Abstractions.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace Etdb.ServiceBase.EventSourcing.Validation
{
    public abstract class CommandValidation<TSourcingCommand> : AbstractValidator<TSourcingCommand>, ICommandValidation<TSourcingCommand> where TSourcingCommand : SourcingCommand
    {
        public abstract bool IsValid(TSourcingCommand sourcingCommand, out ValidationResult validationResult);
    }
}

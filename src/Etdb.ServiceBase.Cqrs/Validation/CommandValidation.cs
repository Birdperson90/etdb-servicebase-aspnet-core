using System;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace Etdb.ServiceBase.Cqrs.Validation
{
    public abstract class CommandValidation<TCommand> : AbstractValidator<TCommand>, ICommandValidation<TCommand>
        where TCommand : class
    {
        public ValidationResult ValidateCommand(TCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return this.Validate(command);
        }

        public async Task<ValidationResult> ValidateCommandAsync(TCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return await this.ValidateAsync(command).ConfigureAwait(false);
        }
    }
}
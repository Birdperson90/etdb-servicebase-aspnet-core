using System;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using Etdb.ServiceBase.Cqrs.Abstractions.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace Etdb.ServiceBase.Cqrs.Validation
{
    public abstract class VoidCommandValidation<TVoidCommad> : AbstractValidator<TVoidCommad>, IVoidCommandValidation<TVoidCommad>
        where TVoidCommad : class, IVoidCommand
    {
        public ValidationResult ValidateCommand(TVoidCommad command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return this.Validate(command);
        }

        public async Task<ValidationResult> ValidateCommandAsync(TVoidCommad command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return await this.ValidateAsync(command).ConfigureAwait(false);
        }
    }
}
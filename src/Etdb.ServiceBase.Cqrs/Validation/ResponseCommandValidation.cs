using System;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using Etdb.ServiceBase.Cqrs.Abstractions.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace Etdb.ServiceBase.Cqrs.Validation
{
    public abstract class ResponseCommandValidation<TResponseCommand, TResponse> : AbstractValidator<TResponseCommand>, IResponseCommandValidation<TResponseCommand, TResponse>
        where TResponseCommand : class, IResponseCommand<TResponse>
    {
        public ValidationResult ValidateCommand(TResponseCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return this.Validate(command);
        }

        public async Task<ValidationResult> ValidateCommandAsync(TResponseCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return await this.ValidateAsync(command).ConfigureAwait(false);
        }
    }
}
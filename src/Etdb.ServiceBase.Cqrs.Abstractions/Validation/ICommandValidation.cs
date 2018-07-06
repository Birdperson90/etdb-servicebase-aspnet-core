using System.Threading.Tasks;
using FluentValidation.Results;

namespace Etdb.ServiceBase.Cqrs.Abstractions.Validation
{
    public interface ICommandValidation<in TCommand> where TCommand : class
    {
        ValidationResult ValidateCommand(TCommand command);

        Task<ValidationResult> ValidateCommandAsync(TCommand command);
    }
}
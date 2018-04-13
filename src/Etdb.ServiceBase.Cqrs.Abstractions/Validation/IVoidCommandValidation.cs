using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using FluentValidation.Results;

namespace Etdb.ServiceBase.Cqrs.Abstractions.Validation
{
    public interface IVoidCommandValidation<in TVoidCommand> where TVoidCommand : IVoidCommand
    {
        ValidationResult ValidateCommand(TVoidCommand command);
        
        Task<ValidationResult> ValidateCommandAsync(TVoidCommand command);
    }
}
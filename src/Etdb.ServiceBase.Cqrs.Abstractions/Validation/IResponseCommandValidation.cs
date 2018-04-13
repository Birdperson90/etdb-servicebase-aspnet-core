using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using FluentValidation.Results;

namespace Etdb.ServiceBase.Cqrs.Abstractions.Validation
{
    public interface IResponseCommandValidation<in TResponseCommand, TResponse> where TResponseCommand : IResponseCommand<TResponse>
    {
        ValidationResult ValidateCommand(TResponseCommand command);
        
        Task<ValidationResult> ValidateCommandAsync(TResponseCommand command);
    }
}
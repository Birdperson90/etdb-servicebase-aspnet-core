using Etdb.ServiceBase.EventSourcing.Abstractions.Commands;
using FluentValidation.Results;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Validation
{
    public interface ICommandValidation<in TTransactionCommand, TResponse> 
        where TTransactionCommand : TransactionCommand<TResponse>
        where TResponse : class
    {
        bool IsValid(TTransactionCommand sourcingCommand, out ValidationResult validationResult);
    }
}

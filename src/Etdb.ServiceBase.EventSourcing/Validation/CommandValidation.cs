using Etdb.ServiceBase.EventSourcing.Abstractions.Commands;
using Etdb.ServiceBase.EventSourcing.Abstractions.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace Etdb.ServiceBase.EventSourcing.Validation
{
    public abstract class CommandValidation<TTransactionCommand, TResponse> : AbstractValidator<TTransactionCommand>, ICommandValidation<TTransactionCommand, TResponse> 
        where TTransactionCommand : TransactionCommand<TResponse>
        where TResponse : class 
    {
        public abstract bool IsValid(TTransactionCommand sourcingCommand, out ValidationResult validationResult);
    }
}

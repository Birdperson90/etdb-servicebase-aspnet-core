using Etdb.ServiceBase.EventSourcing.Abstractions.Commands;
using Etdb.ServiceBase.General.Abstractions.Exceptions;
using MediatR;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Handler
{
    public interface ITransactionHandler<in TTransactionCommand, TResponse> : IRequestHandler<TTransactionCommand, TResponse>
        where TTransactionCommand : TransactionCommand<TResponse>
        where TResponse : class
    {
    }
}

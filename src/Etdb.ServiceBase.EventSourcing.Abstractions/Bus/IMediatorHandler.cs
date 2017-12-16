using System.Threading.Tasks;
using Etdb.ServiceBase.EventSourcing.Abstractions.Commands;
using Etdb.ServiceBase.EventSourcing.Abstractions.Events;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Bus
{
    public interface IMediatorHandler
    {
        Task<TResponse> SendCommand<TTransactionCommand, TResponse>(TTransactionCommand command)
            where TTransactionCommand : TransactionCommand<TResponse>
            where TResponse : class;

        Task<TResponse> SendCommandAsync<TTransactionCommand, TResponse>(TTransactionCommand command)
            where TTransactionCommand : TransactionCommand<TResponse>
            where TResponse : class;

        Task RaiseEvent<T>(T @event) where T : Event;
    }
}

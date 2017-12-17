using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.EventSourcing.Abstractions.Base;
using Etdb.ServiceBase.EventSourcing.Abstractions.Bus;
using Etdb.ServiceBase.EventSourcing.Abstractions.Commands;
using Etdb.ServiceBase.EventSourcing.Abstractions.Handler;
using Etdb.ServiceBase.EventSourcing.Abstractions.Notifications;
using Etdb.ServiceBase.General.Abstractions.Exceptions;

namespace Etdb.ServiceBase.EventSourcing.Handler
{
    public abstract class TransactionHandler<TTransactionCommand, TResponse> : ITransactionHandler<TTransactionCommand, TResponse>
        where TTransactionCommand : TransactionCommand<TResponse>
        where TResponse : class
    {
        private readonly IUnitOfWork unitOfWork;
        protected readonly IMediatorHandler Mediator;

        protected TransactionHandler(IUnitOfWork unitOfWork, IMediatorHandler mediator)
        {
            this.unitOfWork = unitOfWork;
            this.Mediator = mediator;
        }

        public bool CanCommit(out SaveEventstreamException savedEventstreamException)
        {
            savedEventstreamException = null;

            try
            {
                if (this.unitOfWork.IsCommited())
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                savedEventstreamException = new SaveEventstreamException("There was a problem while saving the data!", exception);
            }

            this.Mediator.RaiseEvent(new DomainNotification("Commit", "There was a problem saving the data"));

            savedEventstreamException = new SaveEventstreamException("No data was saved!", null);

            return false;
        }

        public abstract Task<TResponse> Handle(TTransactionCommand request, CancellationToken cancellationToken);
    }
}

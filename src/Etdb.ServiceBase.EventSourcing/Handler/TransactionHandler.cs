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

namespace Etdb.ServiceBase.EventSourcing.Handler
{
    public abstract class TransactionHandler<TTransactionCommand, TResponse> : ITransactionHandler<TTransactionCommand, TResponse>
        where TTransactionCommand : TransactionCommand<TResponse>
        where TResponse : class
    {
        private readonly IUnitOfWork unitOfWork;
        protected readonly IMediatorHandler Mediator;
        private readonly IDomainNotificationHandler<DomainNotification> notificationsHandler;

        protected TransactionHandler(IUnitOfWork unitOfWork, IMediatorHandler mediator, 
            IDomainNotificationHandler<DomainNotification> notificationsHandler)
        {
            this.unitOfWork = unitOfWork;
            this.Mediator = mediator;
            this.notificationsHandler = notificationsHandler;
        }

        public abstract Task<TResponse> Handle(TTransactionCommand request, CancellationToken cancellationToken);

        public bool CanCommit()
        {
            if (this.notificationsHandler.HasNotifications())
            {
                return false;
            }

            if (this.unitOfWork.IsCommited())
            {
                return true;
            }

            this.Mediator.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));

            return false;
        }
    }
}

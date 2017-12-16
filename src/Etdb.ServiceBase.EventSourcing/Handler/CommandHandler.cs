using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.EventSourcing.Abstractions.Base;
using Etdb.ServiceBase.EventSourcing.Abstractions.Bus;
using Etdb.ServiceBase.EventSourcing.Abstractions.Commands;
using Etdb.ServiceBase.EventSourcing.Abstractions.Handler;
using Etdb.ServiceBase.EventSourcing.Abstractions.Notifications;
using FluentValidation.Results;

namespace Etdb.ServiceBase.EventSourcing.Handler
{
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : SourcingCommand
    {
        private readonly IUnitOfWork unitOfWork;
        protected readonly IMediatorHandler Mediator;
        private readonly IDomainNotificationHandler<DomainNotification> notificationsHandler;

        protected CommandHandler(IUnitOfWork unitOfWork, IMediatorHandler mediator, IDomainNotificationHandler<DomainNotification> notificationsHandler)
        {
            this.unitOfWork = unitOfWork;
            this.notificationsHandler = notificationsHandler;
            this.Mediator = mediator;
        }

        public bool Commit()
        {
            if (this.notificationsHandler.HasNotifications())
            {
                return false;
            }

            var commandResponse = unitOfWork.Commit();

            if (commandResponse.Success)
            {
                return true;
            }

            this.Mediator.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
            return false;
        }

        public void NotifyValidationErrors(TCommand message, ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                this.Mediator.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        public abstract Task Handle(TCommand notification, CancellationToken cancellationToken);
    }
}

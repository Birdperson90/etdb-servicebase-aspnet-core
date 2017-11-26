using ETDB.API.ServiceBase.EventSourcing.Abstractions.Base;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Bus;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Commands;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Handler;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Notifications;

namespace ETDB.API.ServiceBase.EventSourcing.Handler
{
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : SourcingCommand
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMediatorHandler bus;
        private readonly IDomainNotificationHandler<DomainNotification> notificationsHandler;

        protected CommandHandler(IUnitOfWork unitOfWork, IMediatorHandler bus, IDomainNotificationHandler<DomainNotification> notificationsHandler)
        {
            this.unitOfWork = unitOfWork;
            this.notificationsHandler = notificationsHandler;
            this.bus = bus;
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

            bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
            return false;
        }

        public void NotifyValidationErrors(TCommand message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        public abstract void Handle(TCommand notification);
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.EventSourcing.Abstractions.Handler;
using Etdb.ServiceBase.EventSourcing.Abstractions.Notifications;

namespace Etdb.ServiceBase.EventSourcing.Handler
{
    public class DomainNotificationHandler<TDomainNotification> : IDomainNotificationHandler<TDomainNotification>
        where TDomainNotification : DomainNotification
    {
        private List<TDomainNotification> notifications;

        public DomainNotificationHandler()
        {
            this.notifications = new List<TDomainNotification>();
        }

        public List<TDomainNotification> GetNotifications()
        {
            return this.notifications;
        }

        public bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public void Dispose()
        {
            notifications = new List<TDomainNotification>();
        }

        public Task Handle(TDomainNotification notification, CancellationToken cancellationToken)
        {
            this.notifications.Add(notification);
            return Task.FromResult(0);
        }
    }
}

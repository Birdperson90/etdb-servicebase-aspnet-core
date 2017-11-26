using System.Collections.Generic;
using System.Linq;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Handler;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Notifications;

namespace ETDB.API.ServiceBase.EventSourcing.Handler
{
    public class DomainNotificationHandler<TDomainNotification> : IDomainNotificationHandler<TDomainNotification>
        where TDomainNotification : DomainNotification
    {
        private List<TDomainNotification> notifications;

        public DomainNotificationHandler()
        {
            this.notifications = new List<TDomainNotification>();
        }

        public void Handle(TDomainNotification notification)
        {
            this.notifications.Add(notification);
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
    }
}

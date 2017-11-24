using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;

namespace ETDB.API.ServiceBase.Domain.Abstractions.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> notifications;

        public DomainNotificationHandler()
        {
            notifications = new List<DomainNotification>();
        }

        public void Handle(DomainNotification message)
        {
            notifications.Add(message);
        }

        public virtual List<DomainNotification> GetNotifications()
        {
            return notifications;
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public void Dispose()
        {
            notifications = new List<DomainNotification>();
        }
    }
}

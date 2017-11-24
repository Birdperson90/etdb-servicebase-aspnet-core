using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;

namespace ETDB.API.ServiceBase.Domain.Abstractions.Notifications
{
    public class DomainNotificationHandler<TNotification> : INotificationHandler<TNotification> 
        where TNotification: DomainNotification
    {
        private List<TNotification> notifications;

        public DomainNotificationHandler()
        {
            notifications = new List<TNotification>();
        }

        public void Handle(TNotification message)
        {
            notifications.Add(message);
        }

        public virtual List<TNotification> GetNotifications()
        {
            return notifications;
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public void Dispose()
        {
            notifications = new List<TNotification>();
        }
    }
}

using System;
using System.Collections.Generic;
using Etdb.ServiceBase.EventSourcing.Abstractions.Notifications;
using MediatR;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Handler
{
    public interface IDomainNotificationHandler<TDomainNotification> : IDisposable, INotificationHandler<TDomainNotification> where TDomainNotification: DomainNotification
    {
        List<TDomainNotification> GetNotifications();

        bool HasNotifications();
    }
}

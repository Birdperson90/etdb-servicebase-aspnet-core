using System;
using System.Collections.Generic;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Notifications;
using MediatR;

namespace ETDB.API.ServiceBase.EventSourcing.Abstractions.Handler
{
    public interface IDomainNotificationHandler<TDomainNotification> : IDisposable, INotificationHandler<TDomainNotification> where TDomainNotification: DomainNotification
    {
        List<TDomainNotification> GetNotifications();

        bool HasNotifications();
    }
}

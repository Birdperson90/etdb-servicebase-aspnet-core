using System;
using System.Collections.Generic;
using MediatR;

namespace ETDB.API.ServiceBase.Domain.Abstractions.Notifications
{
    public interface IDomainNotificationHandler<TDomainNotification> : IDisposable, INotificationHandler<TDomainNotification> where TDomainNotification: DomainNotification
    {
        List<TDomainNotification> GetNotifications();

        bool HasNotifications();
    }
}

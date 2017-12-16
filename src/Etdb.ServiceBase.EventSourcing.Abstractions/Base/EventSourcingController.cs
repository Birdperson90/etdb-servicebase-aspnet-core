using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Etdb.ServiceBase.EventSourcing.Abstractions.ActionResult;
using Etdb.ServiceBase.EventSourcing.Abstractions.Handler;
using Etdb.ServiceBase.EventSourcing.Abstractions.Notifications;
using Etdb.ServiceBase.EventSourcing.Abstractions.Response;
using Microsoft.AspNetCore.Mvc;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Base
{
    public abstract class EventSourcingController : ControllerBase
    {
        private readonly IDomainNotificationHandler<DomainNotification> notificationHandler;

        protected EventSourcingController(IDomainNotificationHandler<DomainNotification> notificationHandler)
        {
            this.notificationHandler = notificationHandler;
        }

        protected IEnumerable<DomainNotification> Notifications
            => notificationHandler.GetNotifications();

        protected bool IsValidOperation()
        {
            return !notificationHandler.HasNotifications();
        }

        protected EventSourcingResult ExecuteResult(IEventSourcingDTO result = null)
        {
            if (this.IsValidOperation())
            {
                return new EventSourcingResult(new EventSourcedResponseSuccess
                {
                    Data = result
                });
            }

            return new EventSourcingResult(new EventSourcedResponseFail
            {
                Errors = this.notificationHandler
                    .GetNotifications()
                    .Select(error => error.Value)
                    .ToArray()
            });
        }

        protected void NotifyModelStateErrors()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                var erroMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            notificationHandler.Handle(new DomainNotification(code, message), CancellationToken.None);
        }
    }
}

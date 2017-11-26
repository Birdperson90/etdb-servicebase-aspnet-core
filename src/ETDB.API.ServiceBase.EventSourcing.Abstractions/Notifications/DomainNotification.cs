using System;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Events;

namespace ETDB.API.ServiceBase.EventSourcing.Abstractions.Notifications
{
    public class DomainNotification : Event
    {
        public Guid DomainNotificationId { get; }
        public string Key { get; }
        public string Value { get; }
        public int Version { get; }

        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}

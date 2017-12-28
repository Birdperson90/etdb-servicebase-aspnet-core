using System;
using MediatR;
using MongoDB.Bson.Serialization.Attributes;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Events
{
    public abstract class Message : INotification
    {
        public string MessageType { get; protected set; }

        [BsonIgnore]
        public Type Type { get; protected set; }
        public string AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
            Type = GetType();
        }
    }
}

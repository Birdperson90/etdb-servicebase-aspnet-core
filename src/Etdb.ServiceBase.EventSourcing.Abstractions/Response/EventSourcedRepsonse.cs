namespace Etdb.ServiceBase.EventSourcing.Abstractions.Response
{
    public abstract class EventSourcedRepsonse
    {
        public bool Success { get; protected set; }
    }
}

namespace ETDB.API.ServiceBase.Controller.Abstractions.Response
{
    public abstract class EventSourcedRepsonse
    {
        public bool Success { get; protected set; }
    }
}

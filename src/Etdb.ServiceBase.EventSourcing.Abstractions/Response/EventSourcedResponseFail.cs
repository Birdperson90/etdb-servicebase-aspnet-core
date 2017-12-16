namespace Etdb.ServiceBase.EventSourcing.Abstractions.Response
{
    public class EventSourcedResponseFail : EventSourcedRepsonse
    {
        public EventSourcedResponseFail()
        {
            this.Success = false;
        }

        public string[] Errors { get; set; }
    }
}

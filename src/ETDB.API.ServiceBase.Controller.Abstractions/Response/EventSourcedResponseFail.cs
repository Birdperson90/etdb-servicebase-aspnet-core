namespace ETDB.API.ServiceBase.Controller.Abstractions.Response
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

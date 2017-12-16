using Etdb.ServiceBase.EventSourcing.Abstractions.Base;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Response
{
    public class EventSourcedResponseSuccess : EventSourcedRepsonse
    {
        public EventSourcedResponseSuccess()
        {
            this.Success = true;
        }

        public IEventSourcingDTO Data { get; set; }
    }
}

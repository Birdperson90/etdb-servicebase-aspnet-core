using System;
using System.Collections.Generic;
using System.Text;

namespace ETDB.API.ServiceBase.Controller.Abstractions
{
    public class EventSourcedResponseSuccess : EventSourcedRepsonse
    {
        public EventSourcedResponseSuccess()
        {
            this.Success = true;
        }

        public EventSourcedDTO Data { get; set; }
    }
}

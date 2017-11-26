using System;
using System.Collections.Generic;
using System.Text;

namespace ETDB.API.ServiceBase.Controller.Abstractions
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

using System;
using System.Collections.Generic;
using System.Text;

namespace ETDB.API.ServiceBase.Domain.Abstractions.Events
{
    public abstract class Event : Message
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}

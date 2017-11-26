using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace ETDB.API.ServiceBase.Controller.Abstractions
{
    public abstract class EventSourcedRepsonse
    {
        public bool Success { get; protected set; }
    }
}

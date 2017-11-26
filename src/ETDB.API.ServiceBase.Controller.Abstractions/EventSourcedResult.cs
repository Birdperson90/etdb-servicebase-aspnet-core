using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ETDB.API.ServiceBase.Controller.Abstractions
{
    public class EventSourcedResult : IActionResult
    {
        private readonly EventSourcedRepsonse eventSourcedRepsonse;

        public EventSourcedResult(EventSourcedRepsonse eventSourcedRepsonse)
        {
            this.eventSourcedRepsonse = eventSourcedRepsonse;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this.eventSourcedRepsonse)
            {
                StatusCode = this.eventSourcedRepsonse.Success
                    ? ((EventSourcedResponseSuccess) this.eventSourcedRepsonse).Data != null
                        ? (int) HttpStatusCode.NoContent
                        : (int) HttpStatusCode.OK
                    : (int) HttpStatusCode.BadRequest
            };

            await result.ExecuteResultAsync(context);
        }
    }
}

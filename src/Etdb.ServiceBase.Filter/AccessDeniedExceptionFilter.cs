using System.Net;
using Etdb.ServiceBase.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Etdb.ServiceBase.Filter
{
    public class AccessDeniedExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<AccessDeniedExceptionFilter> logger;

        public AccessDeniedExceptionFilter(ILogger<AccessDeniedExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!(context.Exception is AccessDeniedException))
            {
                return;
            }

            this.logger.LogError(context.Exception, context.Exception.Message);

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Forbidden;
            context.Result = new ObjectResult(new
            {
                context.Exception.Message
            });
        }
    }
}
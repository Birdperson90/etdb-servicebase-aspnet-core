using System.Net;
using Etdb.ServiceBase.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Etdb.ServiceBase.Filter
{
    public class ResourceNotFoundExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ResourceNotFoundExceptionFilter> logger;

        public ResourceNotFoundExceptionFilter(ILogger<ResourceNotFoundExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!(context.Exception is ResourceNotFoundException))
            {
                return;
            }

            this.logger.LogError(context.Exception, "Resource not found!");

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
            context.Result = new ObjectResult(new
            {
                context.Exception.Message
            });
        }
    }
}
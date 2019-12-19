using System;
using System.Net;
using Etdb.ServiceBase.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Etdb.ServiceBase.Filter
{
    public class ResourceLockedExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ResourceLockedExceptionFilter> logger;

        public ResourceLockedExceptionFilter(ILogger<ResourceLockedExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!(context.Exception is ResourceLockedException exception))
            {
                return;
            }

            this.logger.LogError(exception,
                $"The resource of type {exception.ResourceType.Name} with key {exception.ResourceKey} was busy at {DateTime.UtcNow}");

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Conflict;
            context.Result = new ObjectResult(new
            {
                exception.Message
            });
        }
    }
}
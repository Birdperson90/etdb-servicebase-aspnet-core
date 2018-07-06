using System.Net;
using Etdb.ServiceBase.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Etdb.ServiceBase.Filter
{
    public class ConcurrencyExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ConcurrencyExceptionFilter> logger;

        public ConcurrencyExceptionFilter(ILogger<ConcurrencyExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!(context.Exception is ConcurrencyException))
            {
                return;
            }

            this.logger.LogError(context.Exception, "Conccurency conflict occured!");

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Conflict;

            var exception = (ConcurrencyException) context.Exception;
            context.Result = new ObjectResult(new
            {
                exception.Message,
                exception.Updated
            });
        }
    }
}
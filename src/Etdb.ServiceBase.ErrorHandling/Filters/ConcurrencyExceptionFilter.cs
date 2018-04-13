using Etdb.ServiceBase.ErrorHandling.Abstractions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Etdb.ServiceBase.ErrorHandling.Filters
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
            var exception = (ConcurrencyException) context.Exception;
            context.Result = new BadRequestObjectResult(new
            {
                exception.Message,
                exception.Updated
            });
        }
    }
}

using Etdb.ServiceBase.ErrorHandling.Abstractions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Etdb.ServiceBase.ErrorHandling.Filters
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
            context.Result = new NotFoundObjectResult(new
            {
                context.Exception.Message
            });
        }
    }
}

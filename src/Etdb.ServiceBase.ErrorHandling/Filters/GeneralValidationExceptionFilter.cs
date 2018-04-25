using System.Net;
using Etdb.ServiceBase.ErrorHandling.Abstractions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Etdb.ServiceBase.ErrorHandling.Filters
{
    public class GeneralValidationExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GeneralValidationExceptionFilter> logger;

        public GeneralValidationExceptionFilter(ILogger<GeneralValidationExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!(context.Exception is GeneralValidationException))
            {
                return;
            }

            this.logger.LogError(context.Exception, context.Exception.Message);

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            context.Result = new ObjectResult(new
            {
                context.Exception.Message,
                ((GeneralValidationException) context.Exception).Errors
            });
        }
    }
}

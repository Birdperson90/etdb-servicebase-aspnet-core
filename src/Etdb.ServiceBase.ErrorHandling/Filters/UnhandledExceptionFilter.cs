using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Etdb.ServiceBase.ErrorHandling.Filters
{
    /// <inheritdoc />
    /// <summary>
    /// This filter must be registered first because it will handle every exception.
    /// It will be resolved by the dependency resolver at last.
    /// </summary>
    public class UnhandledExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<UnhandledExceptionFilter> logger;
        private readonly IHostingEnvironment environment;

        public UnhandledExceptionFilter(ILogger<UnhandledExceptionFilter> logger, IHostingEnvironment environment)
        {
            this.logger = logger;
            this.environment = environment;
        }
        
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled)
            {
                return;
            }

            this.logger.LogCritical(context.Exception, "An unhandled exception occured!");
            
            context.ExceptionHandled = true;

            var response = this.environment.IsProduction()
                ? new ObjectResult(new
                {
                    context.Exception.Message
                })
                : new ObjectResult(new
                {
                    context.Exception.Message,
                    context.Exception.StackTrace
                });

            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Result= response;
        }
    }
}
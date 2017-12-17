using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Etdb.ServiceBase.General.Abstractions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Etdb.ServiceBase.General.Abstractions.Filters
{
    public class SaveEventstreamExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<SaveEventstreamExceptionFilter> logger;

        public SaveEventstreamExceptionFilter(ILogger<SaveEventstreamExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!(context.Exception is SaveEventstreamException))
            {
                return;
            }

            this.logger.LogError(context.Exception, context.Exception.Message);

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new
            {
                context.Exception.Message,
                ((SaveEventstreamException)context.Exception).SaveException
            });
        }
    }
}

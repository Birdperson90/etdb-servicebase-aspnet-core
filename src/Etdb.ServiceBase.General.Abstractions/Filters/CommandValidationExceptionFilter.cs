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
    public class CommandValidationExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CommandValidationExceptionFilter> logger;

        public CommandValidationExceptionFilter(ILogger<CommandValidationExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!(context.Exception is CommandValidationException))
            {
                return;
            }

            this.logger.LogError(context.Exception, context.Exception.Message);

            context.ExceptionHandled = true;
            context.Result = new BadRequestObjectResult(new
            {
                context.Exception.Message,
                ((CommandValidationException) context.Exception).Errors
            });
        }
    }
}

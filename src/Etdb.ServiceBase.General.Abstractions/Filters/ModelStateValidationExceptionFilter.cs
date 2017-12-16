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
    public class ModelStateValidationExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ModelStateValidationExceptionFilter> logger;

        public ModelStateValidationExceptionFilter(ILogger<ModelStateValidationExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!(context.Exception is ModelStateValidationException))
            {
                return;
            }

            this.logger.LogError(context.Exception, "Modelstatevalidation failed!");

            context.ExceptionHandled = true;
            var exception = (ModelStateValidationException) context.Exception;
            context.Result = new BadRequestObjectResult(new
            {
                exception.Message,
                exception.Errors
            });
        }
    }
}

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Etdb.ServiceBase.General.Abstractions.Exceptions
{
    public class ModelStateValidationException : Exception
    {
        public string[] Errors { get; }

        public ModelStateValidationException(string message, ModelStateDictionary modelState) : base(message)
        {
            this.Errors = modelState
                .Values
                .SelectMany(value => value.Errors.Select(error => error.ErrorMessage))
                .ToArray();
        }
    }
}

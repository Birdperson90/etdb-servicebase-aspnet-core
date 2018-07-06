using System.Linq;
using Etdb.ServiceBase.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Etdb.ServiceBase.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static GeneralValidationException GenerateValidationException(this ModelStateDictionary modelState,
            string primaryMessage = "Model validation error")
        {
            var errors = modelState.Values.SelectMany(entry => entry.Errors)
                .Select(error => error.ErrorMessage)
                .ToArray();

            errors = errors.Any() ? errors : new[] {"Error parsing object!"};

            return new GeneralValidationException(primaryMessage, errors);
        }
    }
}
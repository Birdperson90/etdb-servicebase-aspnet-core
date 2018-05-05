using System.Linq;
using Etdb.ServiceBase.ErrorHandling.Abstractions.Exceptions;
using FluentValidation.Results;

namespace Etdb.ServiceBase.Extensions
{
    public static class ValidationResultExtensions
    {
        public static GeneralValidationException GenerateValidationException(this ValidationResult validationResult,
            string primaryMessage = "Error during validation!")
        {
            return new GeneralValidationException(primaryMessage,
                validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
        }
    }
}
using System.Linq;
using Etdb.ServiceBase.Exceptions;
using FluentValidation.Results;

namespace Etdb.ServiceBase.Extensions
{
    // ReSharper disable once UnusedType.Global
    public static class ValidationResultExtensions
    {
        public static GeneralValidationException GenerateValidationException(this ValidationResult validationResult,
            string primaryMessage = "Error during validation!")
            => new GeneralValidationException(primaryMessage,
                validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
    }
}
using System;

namespace Etdb.ServiceBase.Exceptions
{
    public class GeneralValidationException : Exception
    {
        public string[] Errors { get; }

        public GeneralValidationException(string message, string[] errors) : base(message)
        {
            this.Errors = errors;
        }
    }
}
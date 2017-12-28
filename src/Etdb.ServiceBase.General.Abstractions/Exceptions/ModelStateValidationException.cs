using System;

namespace Etdb.ServiceBase.General.Abstractions.Exceptions
{
    public class ModelStateValidationException : Exception
    {
        public string[] Errors { get; }

        public ModelStateValidationException(string message, string[] errors) : base(message)
        {
            this.Errors = errors;
        }
    }
}

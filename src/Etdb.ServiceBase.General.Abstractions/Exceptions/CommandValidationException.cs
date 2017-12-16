using System;

namespace Etdb.ServiceBase.General.Abstractions.Exceptions
{
    public class CommandValidationException : Exception
    {
        public string[] Errors { get; }

        public CommandValidationException(string message, string[] errors) : base(message)
        {
            this.Errors = errors;
        }
    }
}

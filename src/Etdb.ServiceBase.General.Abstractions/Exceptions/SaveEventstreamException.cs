using System;
using System.Collections.Generic;
using System.Text;

namespace Etdb.ServiceBase.General.Abstractions.Exceptions
{
    public class SaveEventstreamException : Exception
    {
        public Exception SaveException { get; }

        public SaveEventstreamException(string message, Exception saveException) : base(message)
        {
            this.SaveException = saveException;
        }
    }
}

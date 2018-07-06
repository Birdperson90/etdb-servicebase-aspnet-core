using System;

namespace Etdb.ServiceBase.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public object Updated { get; }

        public ConcurrencyException(string message, object updated) : base(message)
        {
            this.Updated = updated;
        }
    }
}
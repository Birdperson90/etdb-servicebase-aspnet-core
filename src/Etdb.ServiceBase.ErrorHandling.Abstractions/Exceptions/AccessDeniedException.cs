using System;

namespace Etdb.ServiceBase.ErrorHandling.Abstractions.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string message = "You are not permitted to access this resource!") : base(message)
        {
            
        }
    }
}
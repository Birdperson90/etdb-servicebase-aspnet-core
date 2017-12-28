using System;

namespace Etdb.ServiceBase.General.Abstractions.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message)
        {
            
        }
    }
}

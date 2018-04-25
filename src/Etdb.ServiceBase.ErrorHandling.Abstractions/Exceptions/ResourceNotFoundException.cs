using System;

namespace Etdb.ServiceBase.ErrorHandling.Abstractions.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message = "The resource you are looking for could not be found!") : base(message)
        {
            
        }
    }
}

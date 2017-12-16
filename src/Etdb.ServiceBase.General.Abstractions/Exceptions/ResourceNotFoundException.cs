using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Etdb.ServiceBase.General.Abstractions.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message)
        {
            
        }
    }
}

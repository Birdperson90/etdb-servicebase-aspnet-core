using System;
using System.Collections.Generic;
using System.Text;

namespace Etdb.ServiceBase.Exceptions
{
    public class ResourceLockedException : Exception
    {
        public Type ResourceType { get; set; }
        public object ResourceKey { get; set; }

        public ResourceLockedException(Type resourceType, object resourceKey,
            string message = "The resource is currently busy!") : base(message)
        {
            this.ResourceType = resourceType;
            this.ResourceKey = resourceKey;
        }
    }
}
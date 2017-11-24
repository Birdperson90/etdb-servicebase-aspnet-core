using System;
using System.Collections.Generic;
using System.Text;

namespace ETDB.API.ServiceBase.Domain.Abstractions.Events
{
    public interface IHandler<in T> where T : Message
    {
        void Handle(T message);
    }
}

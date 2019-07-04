using System;
using System.Collections.Generic;
using System.Text;

namespace Etdb.ServiceBase.Services.Abstractions
{
    public interface IApplicationUser
    {
        Guid Id { get; }

        string UserName { get; }

        bool IsAuthenticated();
    }
}
using System;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Base
{
    public interface IEventUser
    {
        string UserName { get; }

        Guid UserId { get; }

        bool IsAuthenticated();
    }
}
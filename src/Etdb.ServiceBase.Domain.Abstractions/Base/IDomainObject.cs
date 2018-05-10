using System;

namespace Etdb.ServiceBase.Domain.Abstractions.Base
{
    public interface IDomainObject<out TId> where TId : IEquatable<TId>
    {
        TId Id { get; }
    }
}

using System;

namespace Etdb.ServiceBase.Domain.Abstractions
{
    public interface IDomainObject<TId> where TId : IEquatable<TId>
    {
        TId Id { get; set; }
    }
}

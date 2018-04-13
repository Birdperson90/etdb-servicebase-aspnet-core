using System;

namespace Etdb.ServiceBase.Domain.Abstractions
{
    public interface IDomainObject<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}

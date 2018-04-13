using System;
using Etdb.ServiceBase.Domain.Abstractions;

namespace Etdb.ServiceBase.EntityDomain.Abstractions
{
    public interface IEntity<TKey> : IDomainObject<TKey> where TKey : IEquatable<TKey>
    {
        
    }
}
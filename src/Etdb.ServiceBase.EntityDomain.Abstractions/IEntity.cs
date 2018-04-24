using System;
using Etdb.ServiceBase.Domain.Abstractions;

namespace Etdb.ServiceBase.EntityDomain.Abstractions
{
    public interface IEntity<TId> : IDomainObject<TId> where TId : IEquatable<TId>
    {
        
    }
}
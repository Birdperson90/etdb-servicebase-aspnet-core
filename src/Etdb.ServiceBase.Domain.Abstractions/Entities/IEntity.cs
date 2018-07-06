using System;
using Etdb.ServiceBase.Domain.Abstractions.Base;

namespace Etdb.ServiceBase.Domain.Abstractions.Entities
{
    public interface IEntity<out TId> : IDomainObject<TId> where TId : IEquatable<TId>
    {
    }
}
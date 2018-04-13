using System;
using Etdb.ServiceBase.Domain.Abstractions;

namespace Etdb.ServiceBase.DocumentDomain.Abstractions
{
    public interface IDocument<TKey> : IDomainObject<TKey> where TKey : IEquatable<TKey>
    {
    }
}
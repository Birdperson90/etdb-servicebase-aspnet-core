using System;
using Etdb.ServiceBase.Domain.Abstractions.Base;

namespace Etdb.ServiceBase.Domain.Abstractions.Documents
{
    public interface IDocument<out TKey> : IDomainObject<TKey> where TKey : IEquatable<TKey>
    {
    }
}
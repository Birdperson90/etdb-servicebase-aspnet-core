using System;

namespace Etdb.ServiceBase.DocumentDomain.Abstractions
{
    public interface IPartitionedDocument<TKey> : IDocument<TKey> where TKey : IEquatable<TKey>
    {
        string PartitionKey { get; }
    }
}
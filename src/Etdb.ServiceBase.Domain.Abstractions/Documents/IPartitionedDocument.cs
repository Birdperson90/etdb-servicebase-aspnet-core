using System;

namespace Etdb.ServiceBase.Domain.Abstractions.Documents
{
    public interface IPartitionedDocument<TKey> : IDocument<TKey> where TKey : IEquatable<TKey>
    {
        string PartitionKey { get; set; }
    }
}
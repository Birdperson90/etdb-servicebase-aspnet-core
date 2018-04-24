using System;
using Etdb.ServiceBase.Domain.Abstractions.Documents;

namespace Etdb.ServiceBase.DocumentRepository.Abstractions.Generics
{
    public interface IDocumentRepository<TDocument, in TId> : IReadDocumentRepository<TDocument, TId>, 
        IWriteDocumentRepository<TDocument, TId> where TDocument : class, IDocument<TId> where TId : IEquatable<TId>
    {
    }
}
using System;
using System.Threading.Tasks;
using Etdb.ServiceBase.DocumentDomain.Abstractions;

namespace Etdb.ServiceBase.DocumentRepository.Abstractions.Generics
{
    public interface IWriteDocumentRepository<in TDocument, in TId> 
        where TDocument : class, IDocument<TId> where TId : IEquatable<TId>
    {
        Task AddAsync(TDocument document, string collectionName = null, string partitionKey = null);
        Task<bool> EditAsync(TDocument document, string collectionName = null, string partitionKey = null);
        Task<bool> DeleteAsync(TId id, string collectionName = null, string partitionKey = null);
    }
}
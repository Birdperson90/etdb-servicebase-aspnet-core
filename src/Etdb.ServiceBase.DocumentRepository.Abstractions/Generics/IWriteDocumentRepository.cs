using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Etdb.ServiceBase.DocumentDomain.Abstractions;

namespace Etdb.ServiceBase.DocumentRepository.Abstractions.Generics
{
    public interface IWriteDocumentRepository<in TDocument, in TKey> 
        where TDocument : class, IDocument<TKey> where TKey : IEquatable<TKey>
    {
        Task AddAsync(TDocument document, string collectionName = null, string partitionKey = null);
        Task<bool> EditAsync(TDocument document, string collectionName = null, string partitionKey = null);
        Task<bool> DeleteAsync(TKey id, string collectionName = null, string partitionKey = null);
    }
}
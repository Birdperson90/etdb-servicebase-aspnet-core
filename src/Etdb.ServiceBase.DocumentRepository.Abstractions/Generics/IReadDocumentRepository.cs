using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Etdb.ServiceBase.DocumentDomain.Abstractions;

namespace Etdb.ServiceBase.DocumentRepository.Abstractions.Generics
{
    public interface IReadDocumentRepository<TDocument, in TKey> where TDocument : class, IDocument<TKey> where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TDocument>> GetAllAsync(string collectionName = null, string partitionKey = null);

        Task<IEnumerable<TDocument>> FindAllAsync(Expression<Func<TDocument, bool>> predicate, string collectionName = null, string partitionKey = null);

        Task<TDocument> FindAsync(TKey id, string collectionName = null, string partitionKey = null);

        Task<TDocument> FindAsync(Expression<Func<TDocument, bool>> predicate, string collectionName = null, string partitionKey = null);
    }
}
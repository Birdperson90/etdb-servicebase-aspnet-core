using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Etdb.ServiceBase.Domain.Abstractions.Documents;

namespace Etdb.ServiceBase.DocumentRepository.Abstractions
{
    public interface IWriteDocumentRepository<TDocument, in TId>
        where TDocument : class, IDocument<TId> where TId : IEquatable<TId>
    {
        Task AddAsync(TDocument document, string? collectionName = null, string? partitionKey = null);

        Task AddManyAsync(IEnumerable<TDocument> documents, string? collectionName = null, string? partitionKey = null);

        void Add(TDocument document, string? collectionName = null, string? partitionKey = null);

        void AddMany(IEnumerable<TDocument> documents, string? collectionName = null, string? partitionKey = null);

        Task<bool> EditAsync(TDocument document, string? collectionName = null, string? partitionKey = null);

        bool Edit(TDocument document, string? collectionName = null, string? partitionKey = null);

        Task<bool> DeleteAsync(TId id, string? collectionName = null, string? partitionKey = null);

        Task<bool> DeleteManyAsync(Expression<Func<TDocument, bool>> predicate, string? collectionName = null,
            string? partitionKey = null);

        bool Delete(TId id, string? collectionName = null, string? partitionKey = null);

        bool DeleteMany(Expression<Func<TDocument, bool>> predicate, string? collectionName = null,
            string? partitionKey = null);
    }
}
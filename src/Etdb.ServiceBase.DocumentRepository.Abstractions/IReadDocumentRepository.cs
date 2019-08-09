using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Etdb.ServiceBase.Domain.Abstractions.Documents;

namespace Etdb.ServiceBase.DocumentRepository.Abstractions
{
    public interface IReadDocumentRepository<TDocument, in TId>
        where TDocument : class, IDocument<TId> where TId : IEquatable<TId>
    {
        Task<IEnumerable<TDocument>> GetAllAsync(string? collectionName = null, string? partitionKey = null);

        IEnumerable<TDocument> GetAll(string? collectionName = null, string? partitionKey = null);

        Task<IEnumerable<TDocument>> FindAllAsync(Expression<Func<TDocument, bool>> predicate,
            string? collectionName = null, string? partitionKey = null);

        IEnumerable<TDocument> FindAll(Expression<Func<TDocument, bool>> predicate, string? collectionName = null,
            string? partitionKey = null);

        Task<TDocument?> FindAsync(TId id, string? collectionName = null, string? partitionKey = null);

        TDocument? Find(TId id, string? collectionName = null, string? partitionKey = null);

        Task<TDocument?> FindAsync(Expression<Func<TDocument, bool>> predicate, string? collectionName = null,
            string? partitionKey = null);

        TDocument? Find(Expression<Func<TDocument, bool>> predicate, string? collectionName = null,
            string? partitionKey = null);
    }
}
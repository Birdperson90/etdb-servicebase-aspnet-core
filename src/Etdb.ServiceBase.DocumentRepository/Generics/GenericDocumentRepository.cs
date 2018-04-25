using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Etdb.ServiceBase.DocumentRepository.Abstractions.Context;
using Etdb.ServiceBase.DocumentRepository.Abstractions.Generics;
using Etdb.ServiceBase.Domain.Abstractions.Documents;
using MongoDB.Driver;

namespace Etdb.ServiceBase.DocumentRepository.Generics
{
    public abstract class GenericDocumentRepository<TDocument, TId> : IDocumentRepository<TDocument, TId> 
        where TDocument : class, IDocument<TId>, new()
        where TId : IEquatable<TId>
    {
        private readonly DocumentDbContext context;

        protected GenericDocumentRepository(DocumentDbContext context)
        {
            this.context = context;
        }
        
        public virtual async Task<IEnumerable<TDocument>> GetAllAsync(string collectionName = null, string partitionKey = null)
        {
            return await this.GetCollection(collectionName, partitionKey)
                .AsQueryable()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TDocument>> FindAllAsync(Expression<Func<TDocument, bool>> predicate, 
            string collectionName = null, string partitionKey = null)
        {
            var qursor = await this.GetCollection(collectionName)
                .FindAsync(predicate)
                .ConfigureAwait(false);

            return await qursor.ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<TDocument> FindAsync(TId id, string collectionName = null, string partitionKey = null)
        {
            return await this.GetCollection(collectionName)
                .Find(document => document.Id.Equals(id))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public virtual async Task<TDocument> FindAsync(Expression<Func<TDocument, bool>> predicate, 
            string collectionName = null, string partitionKey = null)
        {
            return await this.GetCollection(collectionName)
                .Find(predicate)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public virtual async Task AddAsync(TDocument document, string collectionName = null, string partitionKey = null)
        {
            await this.GetCollection(collectionName)
                .InsertOneAsync(document)
                .ConfigureAwait(false);
        }

        public virtual async Task<bool> EditAsync(TDocument document, string collectionName = null, string partitionKey = null)
        {
            var editResult = await this.GetCollection(collectionName)
                .UpdateOneAsync(existingDocument => existingDocument.Id.Equals(document.Id),
                    new ObjectUpdateDefinition<TDocument>(document))
                .ConfigureAwait(false);

            return editResult.IsModifiedCountAvailable && 
                   editResult.ModifiedCount == 0;
        }

        public virtual async Task<bool> DeleteAsync(TId id, string collectionName = null, string partitionKey = null)
        {
            var deleteResult = await this.GetCollection(collectionName)
                .DeleteOneAsync(document => document.Id.Equals(id))
                .ConfigureAwait(false);

            return deleteResult.DeletedCount == 0;
        }

        private IMongoCollection<TDocument> GetCollection(string collectionName = null, string partitionKey = null)
        {
            return this.context.GetCollection<TDocument, TId>(collectionName, partitionKey);
        }
    }
}
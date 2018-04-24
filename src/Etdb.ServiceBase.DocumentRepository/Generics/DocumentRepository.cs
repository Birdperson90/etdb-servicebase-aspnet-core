using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Etdb.ServiceBase.DocumentDomain.Abstractions;
using Etdb.ServiceBase.DocumentRepository.Abstractions.Context;
using Etdb.ServiceBase.DocumentRepository.Abstractions.Generics;
using MongoDB.Driver;

namespace Etdb.ServiceBase.DocumentRepository.Generics
{
    public abstract class DocumentRepository<TDocument, TKey> : IDocumentRepository<TDocument, TKey> 
        where TDocument : class, IDocument<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        private readonly DocumentDbContext context;

        protected DocumentRepository(DocumentDbContext context)
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

        public virtual async Task<TDocument> FindAsync(TKey id, string collectionName = null, string partitionKey = null)
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

        public virtual async Task<bool> DeleteAsync(TKey id, string collectionName = null, string partitionKey = null)
        {
            var deleteResult = await this.GetCollection(collectionName)
                .DeleteOneAsync(document => document.Id.Equals(id))
                .ConfigureAwait(false);

            return deleteResult.DeletedCount == 0;
        }

        private IMongoCollection<TDocument> GetCollection(string collectionName = null, string partitionKey = null)
        {
            return this.context.GetCollection<TDocument, TKey>(collectionName, partitionKey);
        }
    }
}
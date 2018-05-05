using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Etdb.ServiceBase.DocumentRepository.Abstractions.Context;
using Etdb.ServiceBase.DocumentRepository.Abstractions.Generics;
using Etdb.ServiceBase.Domain.Abstractions.Documents;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Etdb.ServiceBase.DocumentRepository.Generics
{
    public abstract class GenericDocumentRepository<TDocument, TId> : IDocumentRepository<TDocument, TId>, IDisposable
        where TDocument : class, IDocument<TId>, new()
        where TId : IEquatable<TId>
    {
        private readonly DocumentDbContext context;

        protected GenericDocumentRepository(DocumentDbContext context)
        {
            this.context = context;
        }
        
        public virtual async Task<int> CountAsync(string collectionName = null, string partitionKey = null)
        {
            return await this.GetCollection(collectionName, partitionKey)
                .AsQueryable()
                .Select(document => document.Id)
                .CountAsync();
        }

        public virtual int Count(string collectionName = null, string partitionKey = null)
        {
            return this.GetCollection(collectionName, partitionKey)
                .AsQueryable()
                .Select(document => document.Id)
                .Count();
        }
        
        public virtual async Task<IEnumerable<TDocument>> GetAllAsync(string collectionName = null, string partitionKey = null)
        {
            return await this.GetCollection(collectionName, partitionKey)
                .AsQueryable()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public virtual IEnumerable<TDocument> GetAll(string collectionName = null, string partitionKey = null)
        {
            return this.GetCollection(collectionName, partitionKey)
                .AsQueryable()
                .ToArray();
        }

        public virtual async Task<IEnumerable<TDocument>> FindAllAsync(Expression<Func<TDocument, bool>> predicate, 
            string collectionName = null, string partitionKey = null)
        {
            var qursor = await this.GetCollection(collectionName, partitionKey)
                .FindAsync(predicate)
                .ConfigureAwait(false);

            return await qursor.ToListAsync().ConfigureAwait(false);
        }

        public virtual IEnumerable<TDocument> FindAll(Expression<Func<TDocument, bool>> predicate, string collectionName = null, string partitionKey = null)
        {
            var qursor = this.GetCollection(collectionName, partitionKey)
                .Find(predicate);

            return qursor.ToList();
        }

        public virtual async Task<TDocument> FindAsync(TId id, string collectionName = null, string partitionKey = null)
        {
            return await this.GetCollection(collectionName, partitionKey)
                .Find(document => document.Id.Equals(id))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public virtual TDocument Find(TId id, string collectionName = null, string partitionKey = null)
        {
            return this.GetCollection(collectionName, partitionKey)
                .Find(document => document.Id.Equals(id))
                .SingleOrDefault();
        }

        public virtual async Task<TDocument> FindAsync(Expression<Func<TDocument, bool>> predicate, 
            string collectionName = null, string partitionKey = null)
        {
            return await this.GetCollection(collectionName, partitionKey)
                .Find(predicate)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public virtual TDocument Find(Expression<Func<TDocument, bool>> predicate, string collectionName = null, string partitionKey = null)
        {
            return this.GetCollection(collectionName, partitionKey)
                .Find(predicate)
                .SingleOrDefault();
        }

        public virtual async Task AddAsync(TDocument document, string collectionName = null, string partitionKey = null)
        {
            await this.GetCollection(collectionName, partitionKey)
                .InsertOneAsync(document)
                .ConfigureAwait(false);
        }

        public virtual async Task AddManyAsync(IEnumerable<TDocument> documents, string collectionName = null, string partitionKey = null)
        {
            await this.GetCollection(collectionName, partitionKey)
                .InsertManyAsync(documents)
                .ConfigureAwait(false);
        }

        public virtual void Add(TDocument document, string collectionName = null, string partitionKey = null)
        {
            this.GetCollection(collectionName, partitionKey)
                .InsertOne(document);
        }

        public virtual void AddMany(IEnumerable<TDocument> documents, string collectionName = null,
            string partitionKey = null)
        {
            this.GetCollection(collectionName, partitionKey)
                .InsertMany(documents);
        }

        public virtual async Task<bool> EditAsync(TDocument document, string collectionName = null, string partitionKey = null)
        {
            var editResult = await this.GetCollection(collectionName, partitionKey)
                .ReplaceOneAsync(existingDocument => existingDocument.Id.Equals(document.Id), document)
                .ConfigureAwait(false);

            return editResult.ModifiedCount == 1;
        }

        public bool Edit(TDocument document, string collectionName = null, string partitionKey = null)
        {
            var editResult = this.GetCollection(collectionName, partitionKey)
                .ReplaceOne(existingDocument => existingDocument.Id.Equals(document.Id), document);

            return editResult.ModifiedCount == 1;
        }

        public virtual async Task<bool> DeleteAsync(TId id, string collectionName = null, string partitionKey = null)
        {
            var deleteResult = await this.GetCollection(collectionName, partitionKey)
                .DeleteOneAsync(document => document.Id.Equals(id))
                .ConfigureAwait(false);

            return deleteResult.DeletedCount == 1;
        }
        
        public virtual async Task<bool> DeleteManyAsync(Expression<Func<TDocument, bool>> predicate, string collectionName = null,
            string partitionKey = null)
        {
            var deleteResult = await this.GetCollection(collectionName, partitionKey)
                .DeleteManyAsync(predicate)
                .ConfigureAwait(false);

            return deleteResult.DeletedCount > 0;
        }

        public bool Delete(TId id, string collectionName = null, string partitionKey = null)
        {
            var deleteResult = this.GetCollection(collectionName, partitionKey)
                .DeleteOne(document => document.Id.Equals(id));

            return deleteResult.DeletedCount == 1;
        }
        
        public virtual bool DeleteMany(Expression<Func<TDocument, bool>> predicate, string collectionName = null,
            string partitionKey = null)
        {
            var deleteResult = this.GetCollection(collectionName, partitionKey)
                .DeleteMany(predicate);

            return deleteResult.DeletedCount > 0;
        }

        public virtual IMongoQueryable<TDocument> Query(string collectionName = null, string partitionKey = null)
        {
            return this.GetCollection(collectionName, partitionKey)
                .AsQueryable();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        
        // ReSharper disable once MemberCanBePrivate.Global
        protected IMongoCollection<TDocument> GetCollection(string collectionName = null, string partitionKey = null)
        {
            var possibleCollectionName = string.IsNullOrWhiteSpace(collectionName)
                ? $"{typeof(TDocument).GetTypeInfo().Name.ToLower()}s"
                : collectionName;

            return string.IsNullOrWhiteSpace(partitionKey)
                ? this.context.Database.GetCollection<TDocument>(possibleCollectionName)
                : this.context.Database.GetCollection<TDocument>($"{partitionKey} - {possibleCollectionName}");
        }
    }
}
using System;
using System.Reflection;
using Etdb.ServiceBase.Domain.Abstractions.Documents;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Etdb.ServiceBase.DocumentRepository.Abstractions.Context
{
    public abstract class DocumentDbContext
    {
        protected DocumentDbContext(IOptions<DocumentDbContextOptions> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            this.Database = client.GetDatabase(options.Value.DatabaseName);
        }
        
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public IMongoDatabase Database { get; }

        public IMongoCollection<TDocument> GetCollection<TDocument, TId>(string collectionName = null, 
            string partitionKey = null) where TDocument : class, IDocument<TId> where TId : IEquatable<TId>
        {
            var possibleCollectionName = string.IsNullOrWhiteSpace(collectionName)
                ? $"{typeof(TDocument).GetTypeInfo().Name}s"
                : collectionName;

            return string.IsNullOrWhiteSpace(partitionKey)
                ? this.Database.GetCollection<TDocument>(possibleCollectionName)
                : this.Database.GetCollection<TDocument>($"{partitionKey} - {possibleCollectionName}");
        }
    }
}
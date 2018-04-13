using System;
using System.Reflection;
using Etdb.ServiceBase.DocumentDomain.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Etdb.ServiceBase.DocumentRepository.Abstractions.Context
{
    public abstract class DocumentDbContext
    {
        protected DocumentDbContext(MongoClientSettings settings, string databaseName)
        {
            var client = new MongoClient(settings);
            this.Database = client.GetDatabase(databaseName);
        }
        
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public IMongoDatabase Database { get; }

        public IMongoCollection<TDocument> GetCollection<TDocument, TKey>(string collectionName = null, 
            string partitionKey = null) where TDocument : class, IDocument<TKey> where TKey : IEquatable<TKey>
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
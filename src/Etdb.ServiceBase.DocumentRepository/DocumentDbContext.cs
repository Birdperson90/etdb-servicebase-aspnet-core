using System;
using Etdb.ServiceBase.DocumentRepository.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMemberInSuper.Global

namespace Etdb.ServiceBase.DocumentRepository
{
    public abstract class DocumentDbContext
    {
        protected DocumentDbContext(IOptions<DocumentDbContextOptions> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);

            this.Database = client.GetDatabase(options.Value.DatabaseName);
        }

        protected DocumentDbContext(MongoClientSettings mongoClientSettings, string databaseName)
        {
            if (mongoClientSettings == null) throw new ArgumentNullException(nameof(mongoClientSettings));

            if (string.IsNullOrWhiteSpace(databaseName)) throw new ArgumentNullException(nameof(databaseName));

            var client = new MongoClient(mongoClientSettings);

            this.Database = client.GetDatabase(databaseName);
        }

        protected DocumentDbContext(Func<IMongoDatabase> databaseComposer)
        {
            this.Database = databaseComposer();
        }

        public IMongoDatabase Database { get; }

        public abstract void Configure();


 
    }
}
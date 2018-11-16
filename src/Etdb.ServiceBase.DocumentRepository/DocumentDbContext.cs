using Etdb.ServiceBase.DocumentRepository.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Etdb.ServiceBase.DocumentRepository
{
    public abstract class DocumentDbContext
    {
        protected DocumentDbContext(IOptions<DocumentDbContextOptions> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);

            this.Database = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoDatabase Database { get; }

        public abstract void Configure();

        protected static void UseImmutableConvention()
        {
            ConventionRegistry.Register(nameof(ImmutableTypeClassMapConvention), new ConventionPack
            {
                new ImmutableTypeClassMapConvention()
            }, type => true);
        }

        protected static void UseCamelCaseConvention()
        {
            ConventionRegistry.Register(nameof(CamelCaseElementNameConvention),
                new ConventionPack {new CamelCaseElementNameConvention()},
                type => true);
        }

        protected bool CollectionExists(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);

            var collections = this.Database.ListCollections(new ListCollectionsOptions
            {
                Filter = filter
            });

            return collections.Any();
        }

        protected void CreateCollection(string collectionName, CreateCollectionOptions options = null)
        {
            this.Database.CreateCollection(collectionName, options);
        }
    }
}
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Etdb.ServiceBase.DocumentRepository.Abstractions.Context
{
    public abstract class DocumentDbContext
    {
        private const string CamelCase = "CamelCase";
        
        protected DocumentDbContext(IOptions<DocumentDbContextOptions> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            
            this.Database = client.GetDatabase(options.Value.DatabaseName);
        }

        public abstract void Configure();

        protected static void UseCamelCaseConvention()
        {
            ConventionRegistry.Register(DocumentDbContext.CamelCase, 
                new ConventionPack { new CamelCaseElementNameConvention() }, 
                type => true);
        }
        
        public IMongoDatabase Database { get; }
    }
}
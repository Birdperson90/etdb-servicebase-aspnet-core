using Etdb.ServiceBase.DocumentRepository;
using Etdb.ServiceBase.DocumentRepository.Abstractions;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Documents;
using Microsoft.Extensions.Options;

namespace Etdb.ServiceBase.TestInfrastructure.MongoDb.Context
{
    public class TestDocumentDbContext : DocumentDbContext
    {
        public TestDocumentDbContext(IOptions<DocumentDbContextOptions> options) : base(options)
        {
            this.Configure();
        }

        public sealed override void Configure()
        {
            UseCamelCaseConvention();

            if (!this.CollectionExists($"{nameof(TodoListDocument)}s"))
            {
                this.CreateCollection($"{nameof(TodoListDocument)}s");
            }
        }
    }
}
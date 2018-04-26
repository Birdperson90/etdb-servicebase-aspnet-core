using Etdb.ServiceBase.DocumentRepository.Abstractions.Context;
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

            this.Database.CreateCollection($"{nameof(TodoListDocument)}s");
        }
    }
}
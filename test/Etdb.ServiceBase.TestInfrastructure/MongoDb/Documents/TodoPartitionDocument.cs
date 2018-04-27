using System;
using Etdb.ServiceBase.Domain.Abstractions.Documents;

namespace Etdb.ServiceBase.TestInfrastructure.MongoDb.Documents
{
    public class TodoPartitionDocument : IPartitionedDocument<Guid>
    {
        public Guid Id { get; set; }
        
        public string PartitionKey { get; set; }

        public byte[] HugeBytes { get; set; }
    }
}
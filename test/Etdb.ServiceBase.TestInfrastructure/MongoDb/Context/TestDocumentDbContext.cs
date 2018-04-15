﻿using Etdb.ServiceBase.DocumentRepository.Abstractions.Context;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Etdb.ServiceBase.TestInfrastructure.MongoDb.Context
{
    public class TestDocumentDbContext : DocumentDbContext
    {
        public TestDocumentDbContext(IOptions<DocumentDbContextOptions> options) : base(options)
        {
        }
    }
}
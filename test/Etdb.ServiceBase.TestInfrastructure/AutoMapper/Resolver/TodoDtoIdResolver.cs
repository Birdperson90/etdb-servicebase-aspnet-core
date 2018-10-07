using System;
using AutoMapper;
using Etdb.ServiceBase.TestInfrastructure.AutoMapper.DataTransferObjects;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Documents;

namespace Etdb.ServiceBase.TestInfrastructure.AutoMapper.Resolver
{
    public class TodoDtoIdResolver : IValueResolver<TodoDocument, TodoDto, Guid>
    {
        public Guid Resolve(TodoDocument source, TodoDto destination, Guid destMember, ResolutionContext context)
        {
            return source.Id;
        }
    }
}
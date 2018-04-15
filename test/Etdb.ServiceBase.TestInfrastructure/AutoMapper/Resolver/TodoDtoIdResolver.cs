using System;
using AutoMapper;
using Etdb.ServiceBase.TestInfrastructure.AutoMapper.DataTransferObjects;
using Etdb.ServiceBase.TestInfrastructure.EntityFramework.Entities;

namespace Etdb.ServiceBase.TestInfrastructure.AutoMapper.Resolver
{
    public class TodoDtoIdResolver : IValueResolver<TodoEntity, TodoDto, Guid>
    {
        public Guid Resolve(TodoEntity source, TodoDto destination, Guid destMember, ResolutionContext context)
        {
            return source.Id;
        }
    }
}
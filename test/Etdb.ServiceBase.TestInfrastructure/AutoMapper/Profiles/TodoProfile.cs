using AutoMapper;
using Etdb.ServiceBase.TestInfrastructure.AutoMapper.DataTransferObjects;
using Etdb.ServiceBase.TestInfrastructure.AutoMapper.Resolver;
using Etdb.ServiceBase.TestInfrastructure.EntityFramework.Entities;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Documents;

namespace Etdb.ServiceBase.TestInfrastructure.AutoMapper.Profiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            this.CreateMap<TodoEntity, TodoDto>()
                .ForMember(destination => destination.ValueResolvedId,
                    options => options.ResolveUsing<TodoDtoIdResolver>())
                .ReverseMap();

            this.CreateMap<TodoDocument, TodoDto>()
                .ReverseMap();
        }
    }
}
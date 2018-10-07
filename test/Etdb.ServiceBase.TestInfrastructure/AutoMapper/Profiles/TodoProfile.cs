using AutoMapper;
using Etdb.ServiceBase.TestInfrastructure.AutoMapper.DataTransferObjects;
using Etdb.ServiceBase.TestInfrastructure.AutoMapper.Resolver;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Documents;

namespace Etdb.ServiceBase.TestInfrastructure.AutoMapper.Profiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            this.CreateMap<TodoDocument, TodoDto>()
                .ForMember(destination => destination.ValueResolvedId,
                    options => options.ResolveUsing<TodoDtoIdResolver>())
                .ReverseMap();
        }
    }
}
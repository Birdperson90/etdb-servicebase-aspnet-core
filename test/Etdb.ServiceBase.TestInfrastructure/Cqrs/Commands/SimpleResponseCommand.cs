using Etdb.ServiceBase.Cqrs.Abstractions.Commands;

namespace Etdb.ServiceBase.TestInfrastructure.Cqrs.Commands
{
    public class SimpleResponseCommand : IResponseCommand<SimpleResponse>
    {
        public int Value { get; set; }
    }
}
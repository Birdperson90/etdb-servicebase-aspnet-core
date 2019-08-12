using Etdb.ServiceBase.Cqrs.Abstractions.Commands;

namespace Etdb.ServiceBase.TestInfrastructure.Cqrs.Commands
{
    public class ComplexResponseCommand : IResponseCommand<ComplexResponse>
    {
        public int Value { get; set; }
    }

    public class ComplexResponse
    {
        public int Value { get; set; }
    }
}
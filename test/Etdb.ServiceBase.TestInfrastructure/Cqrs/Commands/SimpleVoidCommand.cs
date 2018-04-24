using Etdb.ServiceBase.Cqrs.Abstractions.Commands;

namespace Etdb.ServiceBase.TestInfrastructure.Cqrs.Commands
{
    public class SimpleVoidCommand : IVoidCommand
    {
        public int Value { get; set; }
    }
}
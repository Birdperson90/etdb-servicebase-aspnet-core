    using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Handler;
using Etdb.ServiceBase.TestInfrastructure.Cqrs.Commands;

namespace Etdb.ServiceBase.TestInfrastructure.Cqrs.Handler
{
    public class SimpleVoidCommandHandler : IVoidCommandHandler<SimpleVoidCommand>
    {
        public Task Handle(SimpleVoidCommand request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
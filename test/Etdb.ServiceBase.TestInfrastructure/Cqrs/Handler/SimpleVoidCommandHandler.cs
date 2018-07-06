using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Handler;
using Etdb.ServiceBase.TestInfrastructure.Cqrs.Commands;
using MediatR;

namespace Etdb.ServiceBase.TestInfrastructure.Cqrs.Handler
{
    public class SimpleVoidCommandHandler : IVoidCommandHandler<SimpleVoidCommand>
    {
        public Task<Unit> Handle(SimpleVoidCommand request, CancellationToken cancellationToken)
        {
            request.Value = 5;

            return Unit.Task;
        }
    }
}
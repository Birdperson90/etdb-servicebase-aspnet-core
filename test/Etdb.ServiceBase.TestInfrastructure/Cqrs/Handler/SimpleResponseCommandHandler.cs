using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Handler;
using Etdb.ServiceBase.TestInfrastructure.Cqrs.Commands;

namespace Etdb.ServiceBase.TestInfrastructure.Cqrs.Handler
{
    public class SimpleResponseCommandHandler : IResponseCommandHandler<SimpleResponseCommand, SimpleResponse>
    {
        public Task<SimpleResponse?> Handle(SimpleResponseCommand request, CancellationToken cancellationToken)
        {
            request.Value = 5;

            return Task.FromResult(new SimpleResponse(10));
        }
    }
}
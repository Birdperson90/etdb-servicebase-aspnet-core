using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Handler;
using Etdb.ServiceBase.Cqrs.Abstractions.Validation;
using Etdb.ServiceBase.Cqrs.Handler;
using Etdb.ServiceBase.ErrorHandling.Abstractions.Exceptions;
using Etdb.ServiceBase.TestInfrastructure.Cqrs.Commands;
using Microsoft.CodeAnalysis.Semantics;

namespace Etdb.ServiceBase.TestInfrastructure.Cqrs.Handler
{
    public class ComplexVoidCommandHandler : VoidCommandHandler<ComplexVoidCommand>
    {
        public ComplexVoidCommandHandler(IVoidCommandValidation<ComplexVoidCommand> commandValidation) : 
            base(commandValidation)
        {
        }

        public override async Task Handle(ComplexVoidCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await this.CommandValidation.ValidateCommandAsync(request);

            if (!validationResult.IsValid)
            {
                throw new GeneralValidationException("", new[]{""});
            }
        }
    }
}
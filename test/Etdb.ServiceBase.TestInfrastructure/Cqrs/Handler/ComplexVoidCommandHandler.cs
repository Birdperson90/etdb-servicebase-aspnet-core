using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Validation;
using Etdb.ServiceBase.Cqrs.Handler;
using Etdb.ServiceBase.Exceptions;
using Etdb.ServiceBase.TestInfrastructure.Cqrs.Commands;
using MediatR;

namespace Etdb.ServiceBase.TestInfrastructure.Cqrs.Handler
{
    public class ComplexVoidCommandHandler : VoidCommandHandler<ComplexVoidCommand>
    {
        public ComplexVoidCommandHandler(ICommandValidation<ComplexVoidCommand> commandValidation) :
            base(commandValidation)
        {
        }

        public override async Task<Unit> Handle(ComplexVoidCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await this.CommandValidation.ValidateCommandAsync(request);

            if (!validationResult.IsValid)
            {
                throw new GeneralValidationException("", new[] {""});
            }

            request.Value = 5;

            return Unit.Value;
        }
    }
}
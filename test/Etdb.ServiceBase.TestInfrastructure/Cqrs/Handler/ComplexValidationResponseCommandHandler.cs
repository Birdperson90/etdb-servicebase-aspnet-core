using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Validation;
using Etdb.ServiceBase.Cqrs.Handler;
using Etdb.ServiceBase.Exceptions;
using Etdb.ServiceBase.TestInfrastructure.Cqrs.Commands;

namespace Etdb.ServiceBase.TestInfrastructure.Cqrs.Handler
{
    public class
        ComplexValidationResponseCommandHandler : ValidationResponseCommandHandler<ComplexResponseCommand,
            ComplexResponse>
    {
        public ComplexValidationResponseCommandHandler(ICommandValidation<ComplexResponseCommand> commandValidation) :
            base(
                commandValidation)
        {
        }

        public override async Task<ComplexResponse> Handle(ComplexResponseCommand request,
            CancellationToken cancellationToken)
        {
            var validationResult = await this.CommandValidation.ValidateCommandAsync(request);

            if (!validationResult.IsValid)
            {
                throw new GeneralValidationException("Error",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }

            request.Value = 5;

            return new ComplexResponse
            {
                Value = 10
            };
        }
    }
}
using Etdb.ServiceBase.Cqrs.Validation;
using Etdb.ServiceBase.TestInfrastructure.Cqrs.Commands;
using FluentValidation;

namespace Etdb.ServiceBase.TestInfrastructure.Cqrs.Validation
{
    public class ComplexResponseCommandValidation : ResponseCommandValidation<ComplexResponseCommand, int>
    {
        public ComplexResponseCommandValidation()
        {
            this.RuleFor(command => command.Value)
                .Equal(0)
                .WithMessage("Must be 0!");
        }
    }
}
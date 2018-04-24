using Etdb.ServiceBase.Cqrs.Validation;
using Etdb.ServiceBase.TestInfrastructure.Cqrs.Commands;
using FluentValidation;

namespace Etdb.ServiceBase.TestInfrastructure.Cqrs.Validation
{
    public class ComplexVoidCommandValidation : VoidCommandValidation<ComplexVoidCommand>
    {
        public ComplexVoidCommandValidation()
        {
            this.RuleFor(command => command.Value)
                .NotEqual(0)
                .WithMessage("Must be zero!");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Commands;
using FluentValidation.Results;

namespace ETDB.API.ServiceBase.EventSourcing.Abstractions.Validation
{
    public interface ICommandValidation<in TCommand> where TCommand : SourcingCommand
    {
        bool IsValid(TCommand sourcingCommand, out ValidationResult validationResult);
    }
}

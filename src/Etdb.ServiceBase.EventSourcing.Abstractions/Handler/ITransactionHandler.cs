using System;
using System.Collections.Generic;
using System.Text;
using Etdb.ServiceBase.EventSourcing.Abstractions.Commands;
using Etdb.ServiceBase.General.Abstractions.Exceptions;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Handler
{
    public interface ITransactionHandler<in TTransactionCommand, TResponse> : IRequestHandler<TTransactionCommand, TResponse>
        where TTransactionCommand : TransactionCommand<TResponse>
        where TResponse : class
    {
        bool CanCommit(out SaveEventstreamException savedEventstreamException);
    }
}

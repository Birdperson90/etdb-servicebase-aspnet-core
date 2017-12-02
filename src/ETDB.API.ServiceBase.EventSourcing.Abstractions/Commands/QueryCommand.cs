using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace ETDB.API.ServiceBase.EventSourcing.Abstractions.Commands
{
    public class QueryCommand : IRequest<Result>
    {
    }
}

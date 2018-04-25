using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Etdb.ServiceBase.TestInfrastructure.Mocks
{
    public class ExceptionContextMock
    {
        public ExceptionContextMock()
        {
            this.ExceptionContext = new ExceptionContext(new ActionContext(new DefaultHttpContext(), 
                    new RouteData(), new ActionDescriptor()), 
                new List<IFilterMetadata>())
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        public ExceptionContext ExceptionContext { get; }
    }
}
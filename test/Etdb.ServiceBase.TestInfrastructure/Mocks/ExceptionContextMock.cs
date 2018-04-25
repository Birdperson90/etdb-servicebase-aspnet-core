using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Etdb.ServiceBase.TestInfrastructure.Mocks
{
    public class ExceptionContextMock
    {
        public ExceptionContextMock()
        {
            this.ActionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
            
            this.ExceptionContext = new ExceptionContext(this.ActionContext, new List<IFilterMetadata>())
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        public ExceptionContext ExceptionContext { get; }

        public ActionContext ActionContext { get; }
        
        public HttpContext HttpContext { get; }
    }
}
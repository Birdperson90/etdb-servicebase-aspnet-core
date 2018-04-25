using System;
using System.Net;
using System.Threading.Tasks;
using Etdb.ServiceBase.ErrorHandling.Abstractions.Exceptions;
using Etdb.ServiceBase.ErrorHandling.Filters;
using Etdb.ServiceBase.TestInfrastructure.Mocks;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Etdb.ServiceBase.ErrorHandling.UnitTests.Filters
{
    public class ResourceNotFoundExceptionFilterTests
    {
        private readonly ExceptionContextMock contextMock;
        private readonly LoggerMock<ResourceNotFoundExceptionFilter> loggerMock;
        private readonly ResourceNotFoundExceptionFilter filter;
        
        public ResourceNotFoundExceptionFilterTests()
        {
            this.contextMock = new ExceptionContextMock();
            this.loggerMock = new LoggerMock<ResourceNotFoundExceptionFilter>();
            this.filter = new ResourceNotFoundExceptionFilter(this.loggerMock.Logger);
        }
        
        [Fact]
        public void ResourceNotFoundExceptionFilter_InputValidException_ExpectNotFoundResponse()
        {
            var exception = new ResourceNotFoundException();

            this.contextMock.ExceptionContext.Exception = exception;
            
            this.filter.OnException(this.contextMock.ExceptionContext);
            
            Assert.True(this.contextMock.ExceptionContext.ExceptionHandled);
            Assert.Equal((int) HttpStatusCode.NotFound, this.contextMock.ExceptionContext.HttpContext.Response.StatusCode);
        }
        
        [Fact]
        public void ResourceNotFoundExceptionFilter_InputInvalidException_ExpectNoChanges()
        {
            var exception = new InvalidOperationException();

            this.contextMock.ExceptionContext.Exception = exception;
            
            this.filter.OnException(this.contextMock.ExceptionContext);
            
            Assert.False(this.contextMock.ExceptionContext.ExceptionHandled);
        }
    }
}
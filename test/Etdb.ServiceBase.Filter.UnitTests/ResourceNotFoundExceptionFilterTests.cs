using System;
using System.Net;
using Etdb.ServiceBase.Exceptions;
using Etdb.ServiceBase.TestInfrastructure.Mocks;
using Xunit;

namespace Etdb.ServiceBase.Filter.UnitTests
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
            Assert.Equal((int) HttpStatusCode.NotFound,
                this.contextMock.ExceptionContext.HttpContext.Response.StatusCode);
        }

        [Fact]
        public void ResourceNotFoundExceptionFilter_InputInvalidException_ExpectNoChanges()
        {
            var exception = new InvalidOperationException();

            this.contextMock.ExceptionContext.Exception = exception;
            var initialStatusCode = this.contextMock.ExceptionContext.HttpContext.Response.StatusCode;

            this.filter.OnException(this.contextMock.ExceptionContext);

            Assert.False(this.contextMock.ExceptionContext.ExceptionHandled);
            Assert.Equal(initialStatusCode, this.contextMock.ExceptionContext.HttpContext.Response.StatusCode);
        }
    }
}
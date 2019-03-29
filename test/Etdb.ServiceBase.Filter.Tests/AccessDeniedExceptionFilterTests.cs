using System;
using System.Net;
using Etdb.ServiceBase.Exceptions;
using Etdb.ServiceBase.TestInfrastructure.Mocks;
using Xunit;

namespace Etdb.ServiceBase.Filter.Tests
{
    public class AccessDeniedExceptionFilterTests
    {
        private readonly ExceptionContextMock contextMock;
        private readonly LoggerMock<AccessDeniedExceptionFilter> loggerMock;
        private readonly AccessDeniedExceptionFilter filter;

        public AccessDeniedExceptionFilterTests()
        {
            this.contextMock = new ExceptionContextMock();
            this.loggerMock = new LoggerMock<AccessDeniedExceptionFilter>();
            this.filter = new AccessDeniedExceptionFilter(this.loggerMock.Logger);
        }

        [Fact]
        public void AccessDeniedExceptionFilter_InputValidException_ExpectForbiddenResponse()
        {
            var exception = new AccessDeniedException();

            this.contextMock.ExceptionContext.Exception = exception;

            this.filter.OnException(this.contextMock.ExceptionContext);

            Assert.True(this.contextMock.ExceptionContext.ExceptionHandled);
            Assert.Equal((int) HttpStatusCode.Forbidden,
                this.contextMock.ExceptionContext.HttpContext.Response.StatusCode);
        }

        [Fact]
        public void AccessDeniedExceptionFilter_InputInvalidException_ExpectNoChanges()
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
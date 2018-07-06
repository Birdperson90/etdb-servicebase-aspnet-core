using System;
using System.Net;
using Etdb.ServiceBase.Exceptions;
using Etdb.ServiceBase.TestInfrastructure.Mocks;
using Xunit;

namespace Etdb.ServiceBase.Filter.UnitTests
{
    public class ConcurrencyExceptionFilterTests
    {
        private readonly ExceptionContextMock contextMock;
        private readonly LoggerMock<ConcurrencyExceptionFilter> loggerMock;
        private readonly ConcurrencyExceptionFilter filter;

        public ConcurrencyExceptionFilterTests()
        {
            this.contextMock = new ExceptionContextMock();
            this.loggerMock = new LoggerMock<ConcurrencyExceptionFilter>();
            this.filter = new ConcurrencyExceptionFilter(this.loggerMock.Logger);
        }

        [Fact]
        public void ConcurrencyExceptionFilter_InputValidException_ExpectConflictResponse()
        {
            var exception = new ConcurrencyException("XY has already been updated", new {SomeNewValue = 123});

            this.contextMock.ExceptionContext.Exception = exception;

            this.filter.OnException(this.contextMock.ExceptionContext);

            Assert.True(this.contextMock.ExceptionContext.ExceptionHandled);
            Assert.Equal((int) HttpStatusCode.Conflict,
                this.contextMock.ExceptionContext.HttpContext.Response.StatusCode);
        }

        [Fact]
        public void ConcurrencyExceptionFilter_InputInvalidException_ExpectNoChanges()
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
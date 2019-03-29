using System;
using System.Net;
using Etdb.ServiceBase.Exceptions;
using Etdb.ServiceBase.TestInfrastructure.Mocks;
using Xunit;

namespace Etdb.ServiceBase.Filter.Tests
{
    public class GeneralValidationExceptionFilterTests
    {
        private readonly ExceptionContextMock contextMock;
        private readonly LoggerMock<GeneralValidationExceptionFilter> loggerMock;
        private readonly GeneralValidationExceptionFilter filter;

        public GeneralValidationExceptionFilterTests()
        {
            this.contextMock = new ExceptionContextMock();
            this.loggerMock = new LoggerMock<GeneralValidationExceptionFilter>();
            this.filter = new GeneralValidationExceptionFilter(this.loggerMock.Logger);
        }

        [Fact]
        public void GeneralValidationExceptionFilter_InputValidException_ExpectBadRequestResponse()
        {
            var exception = new GeneralValidationException("Some validation failed!", new string[] { });

            this.contextMock.ExceptionContext.Exception = exception;

            this.filter.OnException(this.contextMock.ExceptionContext);

            Assert.True(this.contextMock.ExceptionContext.ExceptionHandled);
            Assert.Equal((int) HttpStatusCode.BadRequest,
                this.contextMock.ExceptionContext.HttpContext.Response.StatusCode);
        }

        [Fact]
        public void GeneralValidationExceptionFilter_InputInvalidException_ExpectNoChanges()
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
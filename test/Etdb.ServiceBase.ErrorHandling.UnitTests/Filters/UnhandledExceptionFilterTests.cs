using System;
using System.Net;
using Etdb.ServiceBase.ErrorHandling.Abstractions.Exceptions;
using Etdb.ServiceBase.ErrorHandling.Filters;
using Etdb.ServiceBase.TestInfrastructure.Mocks;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Driver;
using Xunit;

namespace Etdb.ServiceBase.ErrorHandling.UnitTests.Filters
{
    public class UnhandledExceptionFilterTests
    {
        private readonly ExceptionContextMock contextMock;
        private readonly HostingEnvironmentMock environmentMock;
        private readonly LoggerMock<UnhandledExceptionFilter> loggerMock;
        private readonly UnhandledExceptionFilter filter;
        
        public UnhandledExceptionFilterTests()
        {
            this.contextMock = new ExceptionContextMock();
            this.environmentMock = new HostingEnvironmentMock();
            this.loggerMock = new LoggerMock<UnhandledExceptionFilter>();
            this.filter = new UnhandledExceptionFilter(this.loggerMock.Logger, this.environmentMock.Environment);
        }
        
        [Fact]
        public void UnhandledExceptionFilter_InputValidException_ExpectInternalServerError()
        {
            var exception = new Exception();

            this.contextMock.ExceptionContext.Exception = exception;
            
            this.filter.OnException(this.contextMock.ExceptionContext);
            
            Assert.True(this.contextMock.ExceptionContext.ExceptionHandled);
            Assert.Equal((int) HttpStatusCode.InternalServerError, this.contextMock.ExceptionContext.HttpContext.Response.StatusCode);
        }
        
        [Fact]
        public void UnhandledExceptionFilter_InputHandledExceptionContext_ExpectNoChanges()
        {
            var exception = new InvalidOperationException();

            this.contextMock.ExceptionContext.Exception = exception;
            this.contextMock.ExceptionContext.ExceptionHandled = true;
            var initialStatusCode = this.contextMock.ExceptionContext.HttpContext.Response.StatusCode;
            
            this.filter.OnException(this.contextMock.ExceptionContext);
            
            Assert.True(this.contextMock.ExceptionContext.ExceptionHandled);
            Assert.Equal(initialStatusCode, this.contextMock.ExceptionContext.HttpContext.Response.StatusCode);
        }
    }
}
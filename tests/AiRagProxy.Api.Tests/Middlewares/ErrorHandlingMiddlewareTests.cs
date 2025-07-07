using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using AiRagProxy.Api.Middlewares;

namespace AiRagProxy.Api.Tests.Middlewares
{
    public class ErrorHandlingMiddlewareTests
    {
        [Fact]
        public async Task Invoke_NoException_CallsNext()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var context = new DefaultHttpContext();
            var wasCalled = false;
            RequestDelegate next = ctx => { wasCalled = true; return Task.CompletedTask; };
            var middleware = new ErrorHandlingMiddleware(next, loggerMock.Object);

            // Act
            await middleware.Invoke(context);

            // Assert
            Assert.True(wasCalled);
            Assert.NotEqual("application/problem+json", context.Response.ContentType);
        }

        [Fact]
        public async Task Invoke_ExceptionThrown_LogsErrorAndSetsProblemJson()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var context = new DefaultHttpContext();
            RequestDelegate next = ctx => throw new InvalidOperationException("Test error");
            var middleware = new ErrorHandlingMiddleware(next, loggerMock.Object);

            // Act
            await middleware.Invoke(context);

            // Assert
            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("An unhandled exception occurred")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
            Assert.Equal("application/problem+json", context.Response.ContentType);
            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        }
    }
}


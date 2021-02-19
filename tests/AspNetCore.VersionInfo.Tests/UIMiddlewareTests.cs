using AspNetCore.VersionInfo.Middleware;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCore.VersionInfo.Tests
{
    public class UIMiddlewareTests
    {
        [Fact]
        public async Task GetRootAddress_ReturnIndexHtml()
        {
            const string expectedOutput = "Request handled by next request delegate";
            var requestDelegate = new RequestDelegate((innerHttpContext) =>
            {
                innerHttpContext.Response.WriteAsync(expectedOutput);
                return Task.CompletedTask;
            });


            // Arrange
            DefaultHttpContext defaultContext = new DefaultHttpContext();
            defaultContext.Response.Body = new MemoryStream();
            defaultContext.Request.Path = "/";

            // Act
            var middlewareInstance = new UIMiddleware(
                next: requestDelegate,
                loggerFactory: null,
                hostingEnv: null,
                options: null);

            await middlewareInstance.Invoke(defaultContext);

            defaultContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var body = new StreamReader(defaultContext.Response.Body).ReadToEnd();
            
            // Assert
            //Assert.Equal(expectedOutput, body);
        }
    }
}

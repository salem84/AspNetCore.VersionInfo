using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.VersionInfo.Middleware;
using AspNetCore.VersionInfo.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace AspNetCore.VersionInfo.Tests
{
    public class ApiEndpointTests : BaseIocTest
    {
        [Fact]
        public async Task Get()
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

            var infoHandler = new Mock<IInfoCollector>();
            RegisterServiceWithInstance<IInfoCollector>(infoHandler.Object);

            // Act
            var middlewareInstance = new ApiEndpoint(
                next: requestDelegate,
                serviceScopeFactory: _serviceScopeFactory.Object);

            await middlewareInstance.InvokeAsync(defaultContext);

            infoHandler.Verify(x => x.AggregateData(It.IsAny<CancellationToken>()));

            //defaultContext.Response.Body.Seek(0, SeekOrigin.Begin);
            //var body = new StreamReader(defaultContext.Response.Body).ReadToEnd();
            //var jsonData = JsonDocument.Parse(body);
            //var items = jsonData.RootElement.EnumerateObject();

            // Assert
            //Assert.True(items.Count() > 0, "No data in JSON response");
        }
    }
}

using AspNetCore.VersionInfo.Middleware;
using AspNetCore.VersionInfo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCore.VersionInfo.Tests
{
    public class BadgeTest : BaseIocTest
    {
        [Fact]
        public async Task GetStandardBadge()
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
            defaultContext.Request.RouteValues.Add("versionInfoId", "Key1");

            var infoHandler = new Mock<IInfoCollector>();
            var simpleData = new Dictionary<string, string>()
                {
                    { "Key1", "Value1" }
                };
            infoHandler.Setup(x => x.AggregateData()).Returns(simpleData);
            RegisterServiceWithInstance<IInfoCollector>(infoHandler.Object);
            
            var mockLogger = new Mock<ILogger<BadgeEndpoint>>();
            var mockBadgePainter = new Mock<IBadgePainter>();
            var svgReturnForKey1 = "<svg>Value1</svg>";
            mockBadgePainter.Setup(x => x.Draw("Key1", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Style>())).Returns(svgReturnForKey1);
            RegisterServiceWithInstance<IBadgePainter>(mockBadgePainter.Object);

            // Act
            var middlewareInstance = new BadgeEndpoint(
                next: requestDelegate,
                serviceScopeFactory: _serviceScopeFactory.Object,
                logger: mockLogger.Object);

            await middlewareInstance.InvokeAsync(defaultContext);

            // Assert
            defaultContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var body = new StreamReader(defaultContext.Response.Body).ReadToEnd();
            Assert.Equal(svgReturnForKey1, body);
        }

    }
}

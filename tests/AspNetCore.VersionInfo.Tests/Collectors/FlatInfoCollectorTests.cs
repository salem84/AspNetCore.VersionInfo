using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.VersionInfo.Configuration;
using AspNetCore.VersionInfo.Models.Collectors;
using AspNetCore.VersionInfo.Models.Providers;
using AspNetCore.VersionInfo.Providers;
using AspNetCore.VersionInfo.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace AspNetCore.VersionInfo.Tests.Collectors
{
    public class FlatInfoCollectorTests : BaseIocTest
    {
        private readonly Mock<ILogger<FlatInfoCollector>> _mockLogger;
        public FlatInfoCollectorTests()
        {
            _mockLogger = new Mock<ILogger<FlatInfoCollector>>();
        }

        [Fact]
        public async Task InstantiateHandler_ReturnDataInHandler()
        {
            // Arrange
            var infoHandlerSimple = new FakeProvider1();
            RegisterServiceWithInstance(infoHandlerSimple);

            var versionInfoOptions = new VersionInfoOptions()
            {
                Registrations =
                {
                    new ProviderRegistration()
                    {
                        Enabled = true,
                        ProviderType = infoHandlerSimple.GetType()
                    }
                }
            };
            var mockIOptions = new Mock<IOptions<VersionInfoOptions>>();
            mockIOptions.Setup(ap => ap.Value).Returns(versionInfoOptions);

            var collector = new FlatInfoCollector(_serviceScopeFactory.Object, mockIOptions.Object, _mockLogger.Object);

            // Act
            var result = await collector.AggregateData() as FlatCollectorResult;
            var resultDict = result.ToDictionary(includeProviderName: false);

            // Assert
            Assert.Equal(resultDict, infoHandlerSimple.FakeData);
        }

        [Fact]
        public async Task ReturnEmptyData_WhenNoHandler()
        {
            // Arrange
            var versionInfoOptions = new VersionInfoOptions();
            var mockIOptions = new Mock<IOptions<VersionInfoOptions>>();
            mockIOptions.Setup(ap => ap.Value).Returns(versionInfoOptions);
            var collector = new FlatInfoCollector(_serviceScopeFactory.Object, mockIOptions.Object, _mockLogger.Object);

            // Act
            var result = await collector.AggregateData();

            // Assert
            Assert.True(result.Count == 0);
        }

        [Fact]
        public async Task AggregateData_WithMultipleHandler()
        {
            // Arrange
            var infoHandler1 = new FakeProvider1();
            RegisterServiceWithInstance(infoHandler1);

            var infoHandler2 = new FakeProvider2();
            RegisterServiceWithInstance(infoHandler2);

            var versionInfoOptions = new VersionInfoOptions()
            {
                Registrations =
                {
                    new ProviderRegistration()
                    {
                        Enabled = true,
                        ProviderType = infoHandler1.GetType()
                    },
                    new ProviderRegistration()
                    {
                        Enabled = true,
                        ProviderType = infoHandler2.GetType()
                    }
                }
            };
            var mockIOptions = new Mock<IOptions<VersionInfoOptions>>();
            mockIOptions.Setup(ap => ap.Value).Returns(versionInfoOptions);

            var collector = new FlatInfoCollector(_serviceScopeFactory.Object, mockIOptions.Object, _mockLogger.Object);

            // Act
            var result = await collector.AggregateData() as FlatCollectorResult;
            var resultDict = result.ToDictionary(includeProviderName: false);

            // Assert
            var dict = infoHandler1.FakeData.Union(infoHandler2.FakeData).ToDictionary(k => k.Key, v => v.Value);
            Assert.Equal(resultDict, dict);
        }


        public class FakeProvider1 : IInfoProvider
        {
            public string Name { get; } = "fakeProvider1";

            internal Dictionary<string, string> FakeData = new Dictionary<string, string>()
            {
                { "Key1", "Value1" }
            };
            public Task<InfoProviderResult> GetDataAsync() => Task.FromResult(new InfoProviderResult(Name, FakeData));

        }

        public class FakeProvider2 : IInfoProvider
        {
            public string Name { get; } = "fakeProvider2";

            internal Dictionary<string, string> FakeData = new Dictionary<string, string>()
            {
                { "Key2", "Value2" }
            };
            public Task<InfoProviderResult> GetDataAsync() => Task.FromResult(new InfoProviderResult(Name, FakeData));
        }
    }
}

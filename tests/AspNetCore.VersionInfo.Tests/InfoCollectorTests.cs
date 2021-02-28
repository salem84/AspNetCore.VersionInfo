using AspNetCore.VersionInfo.Providers;
using AspNetCore.VersionInfo.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCore.VersionInfo.Tests
{
    public class InfoCollectorTests : BaseIocTest
    {
        private readonly Mock<ILogger<InfoCollector>> _mockLogger;
        public InfoCollectorTests()
        {
            _mockLogger = new Mock<ILogger<InfoCollector>>();
        }

        [Fact]
        public void InstantiateHandler_ReturnDataInHandler()
        {
            // Arrange
            var simpleData = new Dictionary<string, string>()
                {
                    { "Key1", "Value1" }
                };

            var infoHandlerSimple = new Mock<IInfoProvider>();
            infoHandlerSimple.Setup(x => x.GetData())
                .Returns(simpleData);

            var collector = new InfoCollector(new List<IInfoProvider>() { infoHandlerSimple.Object }, _mockLogger.Object);

            // Act
            var result = collector.AggregateData();
            
            // Assert
            Assert.Equal(result, simpleData);
        }

        [Fact]
        public void ReturnEmptyData_WhenNoHandler()
        {
            // Arrange
            var collector = new InfoCollector(new List<IInfoProvider>(), _mockLogger.Object);
            
            // Act
            var result = collector.AggregateData();
            
            // Assert
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void AggregateData_WithMultipleHandler()
        {
            // Arrange
            var simpleData1 = new Dictionary<string, string>()
                {
                    { "Key1", "Value1" }
                };

            var infoHandler1 = new Mock<IInfoProvider>();
            infoHandler1.Setup(x => x.GetData())
                .Returns(simpleData1);

            var simpleData2 = new Dictionary<string, string>()
                {
                    { "Key2", "Value2" }
                };

            var infoHandler2 = new Mock<IInfoProvider>();
            infoHandler2.Setup(x => x.GetData())
                .Returns(simpleData2);

            var collector = new InfoCollector(new List<IInfoProvider>() { infoHandler1.Object, infoHandler2.Object }, _mockLogger.Object);
            
            // Act
            var result = collector.AggregateData();

            // Assert
            var dict = simpleData1.Union(simpleData2).ToDictionary(k => k.Key, v => v.Value);
            Assert.Equal(result, dict);
        }

        [Fact]
        public void AggregateData_WithMultipleHandler_And_DuplicatedKeys()
        {
            // Arrange
            var simpleData1 = new Dictionary<string, string>()
                {
                    { "Key1", "Value1" }
                };

            var infoHandler1 = new Mock<IInfoProvider>();
            infoHandler1.Setup(x => x.GetData())
                .Returns(simpleData1);

            var infoHandler2 = new Mock<IInfoProvider>();
            infoHandler2.Setup(x => x.GetData())
                .Returns(simpleData1);

            var collector = new InfoCollector(new List<IInfoProvider>() { infoHandler1.Object, infoHandler2.Object }, _mockLogger.Object);
            
            // Act
            var result = collector.AggregateData();

            // Assert
            //Assert.Throws<ArgumentException>(collector.AggregateData);
            _mockLogger.VerifyLogging(string.Format(Messages.DUPLICATED_KEY, simpleData1.First().Key), LogLevel.Warning, Times.Once());
            Assert.Equal(result, simpleData1);
        }
    }
}

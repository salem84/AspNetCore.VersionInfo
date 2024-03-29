﻿using System.Collections.Generic;
using System.Linq;
using AspNetCore.VersionInfo.Models.Collectors;
using AspNetCore.VersionInfo.Providers;
using AspNetCore.VersionInfo.Services;
using Microsoft.Extensions.Logging;
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

            infoHandlerSimple.Setup(x => x.Name)
                .Returns(nameof(infoHandlerSimple));

            var collector = new FlatInfoCollector(new List<IInfoProvider>() { infoHandlerSimple.Object }, _mockLogger.Object);

            // Act
            var result = collector.AggregateData() as FlatCollectorResult;
            var resultDict = result.ToDictionary(includeProviderName: false);

            // Assert
            Assert.Equal(resultDict, simpleData);
        }

        [Fact]
        public void ReturnEmptyData_WhenNoHandler()
        {
            // Arrange
            var collector = new FlatInfoCollector(new List<IInfoProvider>(), _mockLogger.Object);

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

            var collector = new FlatInfoCollector(new List<IInfoProvider>() { infoHandler1.Object, infoHandler2.Object }, _mockLogger.Object);

            // Act
            var result = collector.AggregateData() as FlatCollectorResult;
            var resultDict = result.ToDictionary(includeProviderName: false);

            // Assert
            var dict = simpleData1.Union(simpleData2).ToDictionary(k => k.Key, v => v.Value);
            Assert.Equal(resultDict, dict);
        }
    }
}

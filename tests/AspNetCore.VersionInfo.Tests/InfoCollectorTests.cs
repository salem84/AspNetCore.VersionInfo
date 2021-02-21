using AspNetCore.VersionInfo.Services;
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
        [Fact]
        public void InstantiateHandler_ReturnDataInHandler()
        {
            var simpleData = new Dictionary<string, string>()
                {
                    { "Key1", "Value1" }
                };

            var infoHandlerSimple = new Mock<IInfoHandler>();
            infoHandlerSimple.Setup(x => x.GetData())
                .Returns(simpleData);

            var collector = new InfoCollector(new List<IInfoHandler>() { infoHandlerSimple.Object });
            var result = collector.AggregateData();
            Assert.Equal(result, simpleData);
        }

        [Fact]
        public void ReturnEmptyData_WhenNoHandler()
        {
            var collector = new InfoCollector(new List<IInfoHandler>());
            var result = collector.AggregateData();
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void AggregateData_WithMultipleHandler()
        {
            var simpleData1 = new Dictionary<string, string>()
                {
                    { "Key1", "Value1" }
                };

            var infoHandler1 = new Mock<IInfoHandler>();
            infoHandler1.Setup(x => x.GetData())
                .Returns(simpleData1);

            var simpleData2 = new Dictionary<string, string>()
                {
                    { "Key2", "Value2" }
                };

            var infoHandler2 = new Mock<IInfoHandler>();
            infoHandler2.Setup(x => x.GetData())
                .Returns(simpleData2);

            var collector = new InfoCollector(new List<IInfoHandler>() { infoHandler1.Object, infoHandler2.Object });
            var result = collector.AggregateData();

            var dict = simpleData1.Union(simpleData2).ToDictionary(k => k.Key, v => v.Value);
            Assert.Equal(result, dict);
        }

        [Fact]
        public void AggregateData_WithMultipleHandler_And_DuplicatedKeys()
        {
            var simpleData1 = new Dictionary<string, string>()
                {
                    { "Key1", "Value1" }
                };

            var infoHandler1 = new Mock<IInfoHandler>();
            infoHandler1.Setup(x => x.GetData())
                .Returns(simpleData1);

            var infoHandler2 = new Mock<IInfoHandler>();
            infoHandler2.Setup(x => x.GetData())
                .Returns(simpleData1);

            var collector = new InfoCollector(new List<IInfoHandler>() { infoHandler1.Object, infoHandler2.Object });

            Assert.Throws<ArgumentException>(collector.AggregateData);
        }
    }
}

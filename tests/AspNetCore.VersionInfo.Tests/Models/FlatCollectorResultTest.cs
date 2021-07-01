using AspNetCore.VersionInfo.Models;
using AspNetCore.VersionInfo.Models.Collectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCore.VersionInfo.Tests.Models
{
    public class FlatCollectorResultTest
    {
        [Fact]
        public void ConvertToDictionaryTest_WithProviderName()
        {
            // Arrange
            var model = new FlatCollectorResult();
            model.Add(new VersionDataProviderKeyValueResult()
            {
                ProviderName = "Provider1",
                Key = "Key1",
                Value = "Value1"
            });

            // Act
            var dictResult = model.ToDictionary(includeProviderName: true);

            // Assert
            var expectedResult = new Dictionary<string, string>()
            {
                {"Provider1:Key1", "Value1" } 
            };

            Assert.Equal(expectedResult, dictResult);
        }

        [Fact]
        public void ConvertToDictionaryTest_WithoutProviderName()
        {
            // Arrange
            var model = new FlatCollectorResult();
            model.Add(new VersionDataProviderKeyValueResult()
            {
                ProviderName = "Provider1",
                Key = "Key1",
                Value = "Value1"
            });

            // Act
            var dictResult = model.ToDictionary(includeProviderName: false);

            // Assert
            var expectedResult = new Dictionary<string, string>()
            {
                {"Key1", "Value1" }
            };

            Assert.Equal(expectedResult, dictResult);
        }
    }
}

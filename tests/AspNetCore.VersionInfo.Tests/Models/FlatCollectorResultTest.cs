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
        public void TryGetValue_Id_Null()
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
            string id = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => model.TryGetValue(id, out string value));
        }

        [Fact]
        public void TryGetValue_Id_NotExists()
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
            string id = "Provider1:KeyB";
            var success = model.TryGetValue(id, out string value);

            // Assert
            Assert.False(success);
            Assert.Null(value);
        }

        [Fact]
        public void TryGetValue_Id_WithProviderEmpty()
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
            string id = ":Key1";
            var success = model.TryGetValue(id, out string value);

            // Assert
            Assert.False(success);
            Assert.Null(value);
        }

        [Fact]
        public void TryGetValue_Id_WithProvider()
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
            string id = "Provider1:Key1";
            var success = model.TryGetValue(id, out string value);

            // Assert
            Assert.True(success);
            Assert.Equal("Value1", value);
        }

        [Fact]
        public void TryGetValue_Id_WithoutProvider()
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
            string id = "Key1";
            var success = model.TryGetValue(id, out string value);

            // Assert
            Assert.True(success);
            Assert.Equal("Value1", value);
        }

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

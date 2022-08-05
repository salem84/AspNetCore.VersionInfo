using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AspNetCore.VersionInfo.Tests
{
    public class DependencyInjectionTests : BaseIocTest
    {
        [Fact]
        public void VersionInfoBuilder_WithServicesNull()
        {
            // Arrange
            ServiceCollection serviceCollection = null;

            // Act
            Action action = () => serviceCollection.AddVersionInfo();

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}

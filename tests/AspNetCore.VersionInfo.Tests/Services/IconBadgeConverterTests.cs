using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.VersionInfo.Services.Badge;
using Moq;
using Xunit;

namespace AspNetCore.VersionInfo.Tests.Services;
public class IconBadgeConverterTests
{
    [Fact]
    public void FromStringToSvgBase64_Test()
    {
        // Arrange
        var iconString = "<svg></svg>";
        var converter = new IconBadgeConverter();

        // Act
        var result = converter.ConvertToSvgBase64(iconString);

        // Assert
        Assert.StartsWith("data:image/svg+xml;base64,", result);
    }

    [Fact]
    public void FromByteArrayToSvgBase64_Test()
    {
        // Arrange
        var iconBytes = Encoding.UTF8.GetBytes("<svg></svg>");
        var converter = new IconBadgeConverter();

        // Act
        var result = converter.ConvertToSvgBase64(iconBytes);

        // Assert
        Assert.StartsWith("data:image/svg+xml;base64,", result);
    }
}

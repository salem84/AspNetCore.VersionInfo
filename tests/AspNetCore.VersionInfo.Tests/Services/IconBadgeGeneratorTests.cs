using System;
using System.Threading.Tasks;
using AspNetCore.VersionInfo.Services.Badge;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AspNetCore.VersionInfo.Tests.Services;
public class IconBadgeGeneratorTests
{
    private readonly IIconBadgeGenerator _badgeGenerator;
    private readonly Mock<ILogger<IIconBadgeGenerator>> _loggerMock;
    private readonly Mock<IIconBadgeConverter> _converterMock;
    private readonly Mock<IMemoryCache> _memoryCacheMock;
    private readonly Mock<IServiceProvider> _serviceProviderMock;
    private readonly Mock<SimpleIconsDownloader> _downloaderMock;

    public IconBadgeGeneratorTests()
    {
        _loggerMock = new Mock<ILogger<IIconBadgeGenerator>>();
        _converterMock = new Mock<IIconBadgeConverter>();
        _memoryCacheMock = new Mock<IMemoryCache>();
        _serviceProviderMock = new Mock<IServiceProvider>();
        _downloaderMock = new Mock<SimpleIconsDownloader>();

        _serviceProviderMock.Setup(x => x.GetService<SimpleIconsDownloader>())
                            .Returns(_downloaderMock.Object);

        _badgeGenerator = new IconBadgeGenerator(_serviceProviderMock.Object, _loggerMock.Object,
            _converterMock.Object, _memoryCacheMock.Object);
    }

    [Fact]
    public async Task Generate_ValidSlug_ReturnsSvgBase64()
    {
        // Arrange
        var slug = "simpleicons|github";
        var svgDataBytes = new byte[] { 0x1, 0x2, 0x3 };
        var expectedSvgBase64 = "c3Zn";

        _downloaderMock.Setup(x => x.DownloadAsBytes("github"))
                       .ReturnsAsync(svgDataBytes);

        _converterMock.Setup(x => x.ConvertToSvgBase64(svgDataBytes))
                      .Returns(expectedSvgBase64);

        // Act
        var result = await _badgeGenerator.Generate(slug);

        // Assert
        Assert.Equal(expectedSvgBase64, result);
        _loggerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Generate_InvalidSlug_LogsErrorAndReturnsEmptyString()
    {
        // Arrange
        var slug = "invalidslug";
        var expectedErrorMessage = $"Icon slug {slug} is not valid";

        // Act
        var result = await _badgeGenerator.Generate(slug);

        // Assert
        Assert.Equal(string.Empty, result);
        _loggerMock.Verify(x => x.LogError(expectedErrorMessage), Times.Once);
    }

    [Fact]
    public async Task Generate_DownloaderThrowsException_LogsErrorAndReturnsEmptyString()
    {
        // Arrange
        var slug = "simpleicons|github";
        var expectedErrorMessage = "Badge generation error";
        var expectedException = new Exception("Download failed");

        _downloaderMock.Setup(x => x.DownloadAsBytes("github"))
                       .ThrowsAsync(expectedException);

        // Act
        var result = await _badgeGenerator.Generate(slug);

        // Assert
        Assert.Equal(string.Empty, result);
        _loggerMock.Verify(x => x.LogError(expectedException, expectedErrorMessage), Times.Once);
    }

    //[Fact]
    //public void GetDownloader_SupportedType_ReturnsDownloader()
    //{
    //    // Arrange
    //    var expectedDownloader = _downloaderMock.Object;

    //    // Act
    //    var result = _badgeGenerator.GetDownloader("simpleicons");

    //    // Assert
    //    Assert.Equal(expectedDownloader, result);
    //}

    //[Fact]
    //public void GetDownloader_UnsupportedType_ThrowsNotSupportedException()
    //{
    //    // Arrange
    //    var unsupportedType = "unsupported";

    //    // Act & Assert
    //    Assert.Throws<NotSupportedException>(() => _badgeGenerator.GetDownloader(unsupportedType));
    //}
}


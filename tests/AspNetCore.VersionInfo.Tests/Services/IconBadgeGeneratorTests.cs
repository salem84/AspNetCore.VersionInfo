using System;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCore.VersionInfo.Services.Badge;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AspNetCore.VersionInfo.Tests.Services;
public class IconBadgeGeneratorTests
{
    private readonly IIconBadgeGenerator _badgeGenerator;
    private readonly Mock<ILogger<IconBadgeGenerator>> _loggerMock;
    private readonly Mock<IIconBadgeConverter> _converterMock;
    private readonly Mock<IServiceProvider> _serviceProviderMock;
    private readonly Mock<SimpleIconsDownloader> _downloaderMock;

    public IconBadgeGeneratorTests()
    {
        _loggerMock = new Mock<ILogger<IconBadgeGenerator>>();
        _loggerMock.Setup(x => x.IsEnabled(LogLevel.Error)).Returns(true);

        _converterMock = new Mock<IIconBadgeConverter>();
        _serviceProviderMock = new Mock<IServiceProvider>();
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _downloaderMock = new Mock<SimpleIconsDownloader>(httpClientFactoryMock.Object);

        _serviceProviderMock.Setup(x => x.GetService(typeof(SimpleIconsDownloader)))
                            .Returns(_downloaderMock.Object);

        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        _badgeGenerator = new IconBadgeGenerator(_serviceProviderMock.Object, _loggerMock.Object,
            _converterMock.Object, memoryCache);
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
        var expectedLoggedErrorMessage = $"Icon slug not valid: {slug}";

        // Act
        var result = await _badgeGenerator.Generate(slug);

        // Assert
        Assert.Equal(string.Empty, result);
        _loggerMock.VerifyLogging(expectedLoggedErrorMessage, LogLevel.Error, Times.Once());
    }

    [Fact]
    public async Task Generate_DownloaderThrowsException_LogsErrorAndReturnsEmptyString()
    {
        // Arrange
        var slug = "simpleicons|github";
        var expectedLoggedErrorMessage = "Badge generation error";
        var expectedException = new Exception("Download failed");

        _downloaderMock.Setup(x => x.DownloadAsBytes("github"))
                       .ThrowsAsync(expectedException);

        // Act
        var result = await _badgeGenerator.Generate(slug);

        // Assert
        Assert.Equal(string.Empty, result);
        _loggerMock.VerifyLogging(expectedLoggedErrorMessage, LogLevel.Error, Times.Once());
    }

    [Fact]
    public async Task Generate_UnsupportedIconBadgeDownloader()
    {
        // Arrange
        var slug = "unsupported|github";
        var expectedLoggedErrorMessage = "Badge generation error";
        var expectedLoggedException = new NotSupportedException("Icon type not supported");

        // Act
        var result = await _badgeGenerator.Generate(slug);

        // Assert
        Assert.Equal(string.Empty, result);
        _loggerMock.VerifyLogging(expectedLoggedErrorMessage, expectedLoggedException, LogLevel.Error, Times.Once());
    }
}


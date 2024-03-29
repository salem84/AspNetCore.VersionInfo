﻿using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.VersionInfo.Services.Badge;
using Moq;
using Moq.Protected;
using Xunit;

namespace AspNetCore.VersionInfo.Tests
{
    public class SimpleIconsBadgeDownloaderTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private static readonly string _validIconSlug = "githubiconslug";
        private static readonly byte[] _validIconData = Encoding.UTF8.GetBytes(_validIconSlug);

        public SimpleIconsBadgeDownloaderTests()
        {
            var expectedUrl = $"https://cdn.simpleicons.org/{_validIconSlug}/white";

            _httpClientFactoryMock = new Mock<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.Equals(expectedUrl, StringComparison.OrdinalIgnoreCase)), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ByteArrayContent(_validIconData),
                });

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(x => !x.RequestUri.AbsoluteUri.Equals(expectedUrl, StringComparison.OrdinalIgnoreCase)), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new ByteArrayContent(Array.Empty<byte>()),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        }

        [Fact]
        public async Task GenerateFromSimpleIcons_WithValidIcon()
        {
            // Arrange
            var downloader = new SimpleIconsDownloader(_httpClientFactoryMock.Object);

            // Act
            var result = await downloader.DownloadAsBytes(_validIconSlug);

            // Assert
            Assert.Equal(_validIconData, result);
            _httpClientFactoryMock.Verify(x => x.CreateClient(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GenerateFromSimpleIcons_WithFakeIcon()
        {
            // Arrange
            var downloader = new SimpleIconsDownloader(_httpClientFactoryMock.Object);

            // Act
            await Assert.ThrowsAsync<HttpRequestException>(() => downloader.DownloadAsBytes("fakeicon"));

            // Assert
            _httpClientFactoryMock.Verify(x => x.CreateClient(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GenerateFromSimpleIcons_WithNotValidSlug()
        {
            // Arrange
            var downloader = new SimpleIconsDownloader(_httpClientFactoryMock.Object);

            // Act
            await Assert.ThrowsAsync<ArgumentException>(() => downloader.DownloadAsBytes("http://fakeurl/icon"));

            // Assert
            _httpClientFactoryMock.Verify(x => x.CreateClient(It.IsAny<string>()), Times.Never);
        }
    }
}

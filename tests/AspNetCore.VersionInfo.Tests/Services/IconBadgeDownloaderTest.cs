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
    public class IconBadgeDownloaderTest : BaseIocTest
    {
        [Fact]
        public async Task GenerateFromSimpleIcons()
        {
            // Arrange
            var iconSlug = "valid-icon-slug";
            var iconDataPrefix = new byte[] { 0x0, 0x1, 0x2, 0x3 };
            var iconData = Encoding.UTF8.GetBytes(iconSlug);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ByteArrayContent(iconData),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            //var expectedUrl = $"https://cdn.simpleicons.org/{iconSlug}/white";
            //var expectedData = new byte[] { 1, 2, 3 };
            //var httpClient = new HttpClient();
            //var response = new HttpResponseMessage();
            //response.Content = new ByteArrayContent(expectedData);
            //_httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            ////httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("unit-test");
            ////httpClient.DefaultRequestHeaders.Accept.TryParseAdd("*/*");
            ////httpClient.DefaultRequestHeaders.Add("Referer", "https://localhost/");
            ////httpClient.DefaultRequestHeaders.Add("Origin", "https://localhost/");

            var downloader = new SimpleIconsDownloader(httpClientFactoryMock.Object);

            // Act
            var result = await downloader.DownloadAsBytes(iconSlug);

            // Assert
            Assert.Equal(iconData, result);
            httpClientFactoryMock.Verify(x => x.CreateClient(It.IsAny<string>()), Times.Once);
            //httpClient.Dispose();
        }
    }
}

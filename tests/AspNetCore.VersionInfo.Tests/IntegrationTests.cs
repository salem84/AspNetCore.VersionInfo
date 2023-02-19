using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCore.VersionInfo.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public async Task VersionApiUrl_ReturnsJson()
        {
            var client = new TestSite(typeof(Samples.Basic.Startup)).BuildClient();

            var indexResponse = await client.GetAsync(Constants.DEFAULT_API_ENDPOINT_URL);

            var body = await indexResponse.Content.ReadAsStringAsync();
            var jsonData = JsonDocument.Parse(body);
            var items = jsonData.RootElement.EnumerateObject();

            // Assert
            Assert.Equal(HttpStatusCode.OK, indexResponse.StatusCode);
            Assert.True(items.Any(), "No data in JSON response");

            // TODO check type returned?
        }

        [Fact]
        public async Task VersionUrl_ReturnsHtml()
        {
            var client = new TestSite(typeof(Samples.Basic.Startup)).BuildClient();

            var indexResponse = await client.GetAsync(Constants.DEFAULT_HTML_ENDPOINT_URL);

            var body = await indexResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, indexResponse.StatusCode);
            Assert.Contains("<h1>Version Information</h1>", body);
        }

        [Fact]
        public async Task VersionUrl_WithJsRequest_ReturnsNotFound()
        {
            var client = new TestSite(typeof(Samples.Basic.Startup)).BuildClient();

            var indexResponse = await client.GetAsync($"{Constants.DEFAULT_HTML_ENDPOINT_URL}/vue.js");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, indexResponse.StatusCode);
        }

        [Fact]
        public async Task VersionApiUrl_WithCustomUrl_ReturnsJson()
        {
            var client = new TestSite(typeof(Samples.CustomOptions.Startup)).BuildClient();

            var indexResponse = await client.GetAsync(Samples.CustomOptions.Startup.CUSTOM_JSON_URL);

            var body = await indexResponse.Content.ReadAsStringAsync();
            var jsonData = JsonDocument.Parse(body);
            var items = jsonData.RootElement.EnumerateObject();

            // Assert
            Assert.Equal(HttpStatusCode.OK, indexResponse.StatusCode);
            Assert.True(items.Count() > 0, "No data in JSON response");
        }

        [Fact]
        public async Task VersionUrl_WithCustomUrl_ReturnsHtml()
        {
            var client = new TestSite(typeof(Samples.CustomOptions.Startup)).BuildClient();

            var indexResponse = await client.GetAsync(Samples.CustomOptions.Startup.CUSTOM_HTML_URL);

            var body = await indexResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, indexResponse.StatusCode);
            Assert.Contains("<h1>Version Information</h1>", body);
        }

        [Fact]
        public async Task BadgeUrl_ReturnsSvg()
        {
            // Arrange
            var client = new TestSite(typeof(Samples.Basic.Startup)).BuildClient();

            // Act
            var versionInfoId = Constants.KEY_ENTRY_ASSEMBLY_VERSION;
            var url = Constants.DEFAULT_BADGE_ENDPOINT_URL.Replace("{versionInfoId}", versionInfoId);
            var indexResponse = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, indexResponse.StatusCode);
            Assert.Equal(Constants.DEFAULT_BADGE_RESPONSE_CONTENT_TYPE, indexResponse.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task BadgeUrl_WithKeyNotValid_ReturnsNotFound()
        {
            // Arrange
            var client = new TestSite(typeof(Samples.Basic.Startup)).BuildClient();

            // Act
            var versionInfoId = "KEY_NOT_VALID";
            var url = Constants.DEFAULT_BADGE_ENDPOINT_URL.Replace("{versionInfoId}", versionInfoId);
            var indexResponse = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, indexResponse.StatusCode);
        }

        [Fact]
        public async Task BadgeUrl_WithKeyEmpty_ReturnsNotFound()
        {
            // Arrange
            var client = new TestSite(typeof(Samples.Basic.Startup)).BuildClient();

            // Act
            var versionInfoId = "";
            var url = Constants.DEFAULT_BADGE_ENDPOINT_URL.Replace("{versionInfoId}", versionInfoId);
            var indexResponse = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, indexResponse.StatusCode);
        }
    }
}

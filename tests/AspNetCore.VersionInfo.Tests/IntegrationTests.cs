using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
            Assert.True(items.Count() > 0, "No data in JSON response");
        }

        [Fact]
        public async Task VersionUrl_ReturnsHtml()
        {
            var client = new TestSite(typeof(Samples.Basic.Startup)).BuildClient();

            var indexResponse = await client.GetAsync(Constants.DEFAULT_HTML_ENDPOINT_URL);

            var body = await indexResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, indexResponse.StatusCode);
            Assert.Contains("<h1>Versions</h1>", body);
        }
    }
}

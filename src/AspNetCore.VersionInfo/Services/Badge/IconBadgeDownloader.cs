using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Services.Badge
{
    internal interface IIconBadgeDownloader
    {
        Task<byte[]> DownloadAsBytes(string iconSlug);
    }

    internal class SimpleIconsDownloader : IIconBadgeDownloader
    {
        private const string BASE_URL = "https://cdn.simpleicons.org";
        private readonly IHttpClientFactory _httpClientFactory;

        public SimpleIconsDownloader(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<byte[]> DownloadAsBytes(string iconSlug)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            var iconUrl = $"{BASE_URL}/{iconSlug}/white";
            return await httpClient.GetByteArrayAsync(iconUrl);
        }
    }
}


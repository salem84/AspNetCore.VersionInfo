using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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

        private static Regex ValidSlugRegex = new (
            pattern: "^[a-zA-Z0-9]*$",
            options: RegexOptions.Compiled | RegexOptions.IgnoreCase,
            matchTimeout: TimeSpan.FromMilliseconds(200));

        private readonly IHttpClientFactory _httpClientFactory;

        public SimpleIconsDownloader(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<byte[]> DownloadAsBytes(string iconSlug)
        {
            // Validate input
            if(!ValidSlugRegex.IsMatch(iconSlug))
            {
                throw new ArgumentException("Not valid icon slug", nameof(iconSlug));
            }

            // Download icon
            using var httpClient = _httpClientFactory.CreateClient();
            var iconUrl = $"{BASE_URL}/{iconSlug}/white";
            return await httpClient.GetByteArrayAsync(iconUrl);
        }
    }
}


﻿using System;
using System.Net.Http;
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
#pragma warning disable S1075 // URIs should not be hardcoded    
        private const string BASE_URL = "https://cdn.simpleicons.org";
#pragma warning restore S1075 // URIs should not be hardcoded    

        private static Regex ValidSlugRegex = new(
            pattern: "^[a-zA-Z0-9]*$",
            options: RegexOptions.Compiled | RegexOptions.IgnoreCase,
            matchTimeout: TimeSpan.FromMilliseconds(200));

        private readonly IHttpClientFactory _httpClientFactory;

        public SimpleIconsDownloader(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public virtual async Task<byte[]> DownloadAsBytes(string iconSlug)
        {
            // Validate input
            if (!ValidSlugRegex.IsMatch(iconSlug))
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


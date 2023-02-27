﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCore.VersionInfo.Services.Badge
{
    internal interface IIconBadgeGenerator
    {
        Task<string> Generate(string iconSlug);
    }

    internal class IconBadgeGenerator : IIconBadgeGenerator
    {
        private const string TYPE_SIMPLEICONS = "simpleicons";
        private const char TYPE_SEPARATOR = '|';
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<IconBadgeGenerator> logger;
        private readonly IIconBadgeConverter iconBadgeConverter;
        private readonly IMemoryCache memoryCache;

        public IconBadgeGenerator(IServiceProvider serviceProvider,
            ILogger<IconBadgeGenerator> logger,
            IIconBadgeConverter iconBadgeConverter,
            IMemoryCache memoryCache
            )
        {
            _serviceProvider = serviceProvider;
            this.logger = logger;
            this.iconBadgeConverter = iconBadgeConverter;
            this.memoryCache = memoryCache;
        }
        public async Task<string> Generate(string iconSlug)
        {
            try
            {
                var iconInfoArray = iconSlug.Split(TYPE_SEPARATOR);
                if(iconInfoArray.Length != 2)
                {
                    logger.LogError($"Icon slug {iconSlug} is not valid");
                    return string.Empty;
                }

                var iconType = iconInfoArray[0];
                var iconName = iconInfoArray[1];
                var downloader = GetDownloader(iconType);
                var svgDataBytes = await downloader.DownloadAsBytes(iconName);
                var svgBase64 = iconBadgeConverter.ConvertToSvgBase64(svgDataBytes);

                return svgBase64;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Badge generation error");
                return string.Empty;
            }
        }

        private IIconBadgeDownloader GetDownloader(string iconType)
        {
            switch(iconType)
            {
                case TYPE_SIMPLEICONS:
                    return _serviceProvider.GetService<SimpleIconsDownloader>();
                default:
                    throw new NotSupportedException("Icon type not supported");
            }
        }
    }
}
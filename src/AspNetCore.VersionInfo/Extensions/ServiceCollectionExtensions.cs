using System;
using AspNetCore.VersionInfo;
using AspNetCore.VersionInfo.Configuration;
using AspNetCore.VersionInfo.Services;
using AspNetCore.VersionInfo.Services.Badge;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static VersionInfoBuilder AddVersionInfo(this IServiceCollection services,
            Action<VersionInfoSettings> setupSettings = null)
        {
            var builder = new VersionInfoBuilder(services);

            services.AddHttpClient();

            services.AddTransient<IInfoCollector, FlatInfoCollector>();
            services.AddTransient<IBadgePainter, BadgePainter>();
            services.AddTransient<IIconBadgeGenerator, IconBadgeGenerator>();
            services.AddTransient<IIconBadgeConverter, IconBadgeConverter>();

            services.AddTransient<SimpleIconsDownloader>();

            return builder;
        }
    }
}

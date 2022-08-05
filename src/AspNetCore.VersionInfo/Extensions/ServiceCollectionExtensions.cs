using AspNetCore.VersionInfo;
using AspNetCore.VersionInfo.Configuration;
using AspNetCore.VersionInfo.Services;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static VersionInfoBuilder AddVersionInfo(this IServiceCollection services,
            Action<VersionInfoSettings> setupSettings = null)
        {
            var builder = new VersionInfoBuilder(services);

            services.AddTransient<IInfoCollector, FlatInfoCollector>();
            services.AddTransient<IBadgePainter, BadgePainter>();

            return builder;
        }
    }
}

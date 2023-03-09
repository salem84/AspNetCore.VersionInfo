using System;
using AspNetCore.VersionInfo.Configuration;
using AspNetCore.VersionInfo.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.VersionInfo
{
    public static class VersionInfoBuilderExtensions
    {
        public static VersionInfoBuilder With<T>(this VersionInfoBuilder builder, Action<ProviderOptions> setupOptions = null) where T : class, IInfoProvider
        {
            var options = new ProviderOptions();
            setupOptions?.Invoke(options);

            return ConfigureProvider<T>(builder, options);
        }

        private static VersionInfoBuilder ConfigureProvider<T>(this VersionInfoBuilder builder, ProviderOptions options) where T : class, IInfoProvider
        {
            builder.Services.AddTransient<IInfoProvider, T>();
            return builder;
        }
    }
}

using System;
using AspNetCore.VersionInfo.Configuration;
using AspNetCore.VersionInfo.Models.Providers;
using AspNetCore.VersionInfo.Providers;

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
            builder.Add(new ProviderRegistration()
            {
                ProviderType = typeof(T),
                Enabled = true,
                Options = options
            });
            return builder;
        }
    }
}

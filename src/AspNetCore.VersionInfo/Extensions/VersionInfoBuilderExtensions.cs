using AspNetCore.VersionInfo.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.VersionInfo
{
    public static class VersionInfoBuilderExtensions
    {
        public static VersionInfoBuilder With<T>(this VersionInfoBuilder builder) where T : class, IInfoProvider
        {
            builder.Services.AddTransient<IInfoProvider, T>();
            return builder;
        }
    }
}

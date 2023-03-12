using System;
using AspNetCore.VersionInfo.Configuration;
using AspNetCore.VersionInfo.Models.Providers;
using AspNetCore.VersionInfo.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.VersionInfo
{
    public class VersionInfoBuilder
    {
        public IServiceCollection Services { get; }
        public VersionInfoBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public VersionInfoBuilder Add(ProviderRegistration registration)
        {
            Services.Configure<VersionInfoOptions>(options =>
            {
                options.Registrations.Add(registration);
            });

            Services.AddTransient(typeof(IInfoProvider), registration.ProviderType);
            Services.AddTransient(registration.ProviderType);

            return this;
        }
    }
}

using System;
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
    }
}

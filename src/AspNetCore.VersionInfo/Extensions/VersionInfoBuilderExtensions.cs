using AspNetCore.VersionInfo.Providers;
using AspNetCore.VersionInfo.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

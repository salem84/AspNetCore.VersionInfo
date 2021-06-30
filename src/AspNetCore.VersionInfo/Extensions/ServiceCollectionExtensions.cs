﻿using AspNetCore.VersionInfo;
using AspNetCore.VersionInfo.Configuration;
using AspNetCore.VersionInfo.Models;
using AspNetCore.VersionInfo.Providers;
using AspNetCore.VersionInfo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

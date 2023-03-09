using System;
using System.Collections.Generic;
using AspNetCore.VersionInfo.Configuration;
using AspNetCore.VersionInfo.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.VersionInfo
{
    public static class VersionInfoBuilderExtensions
    {
        private static List<String> keyValues = new List<String>();
        public static VersionInfoBuilder With<T>(this VersionInfoBuilder builder, Action<ExclusionSettings> configureOptions = null) where T : class, IInfoProvider
        {

            if (configureOptions != null)
            {
                var settings = new ExclusionSettings();

                configureOptions(settings);

                foreach (var keyItem in settings.Keys)
                {
                    keyValues.Add(keyItem);
                }
            }

            builder.Services.AddTransient<IExclusionSettings>(serviceProvider =>
            {

                return new ExclusionSettingsService(keyValues);

            });

            builder.Services.AddTransient<IInfoProvider, T>();
            return builder;
        }
    }
}

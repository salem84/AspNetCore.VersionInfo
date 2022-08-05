using System;
using System.Collections.Generic;
using AspNetCore.VersionInfo.Configuration;
using AspNetCore.VersionInfo.Endpoint;
using AspNetCore.VersionInfo.Middleware;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.AspNetCore.Builder
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapVersionInfo(this IEndpointRouteBuilder builder,
            Action<VersionInfoOptions> setupOptions = null)
        {
            var options = new VersionInfoOptions();
            setupOptions?.Invoke(options);

            var apiDelegate =
                builder.CreateApplicationBuilder()
                    .UseMiddleware<ApiEndpoint>()
                    .Build();

            var apiEndpoint = builder.Map(options.ApiPath, apiDelegate)
                .WithDisplayName("VersionInfo API");

            var uiDelegate =
                builder.CreateApplicationBuilder()
                    .UseMiddleware<HtmlEndpoint>()
                    .Build();

            var uiEndpoint = builder.Map(options.HtmlPath, uiDelegate)
                .WithDisplayName("VersionInfo HTML");

            var badgeDelegate =
                builder.CreateApplicationBuilder()
                    .UseMiddleware<BadgeEndpoint>()
                    .Build();

            var badgeEndpoint = builder.Map(options.BadgePath, badgeDelegate)
                .WithDisplayName("VersionInfo Badge");

            var endpointConventionBuilders = new List<IEndpointConventionBuilder>(new[] { apiEndpoint, uiEndpoint, badgeEndpoint });
            return new VersionInfoConventionBuilder(endpointConventionBuilders);
        }
    }
}

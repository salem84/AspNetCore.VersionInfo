using AspNetCore.VersionInfo.Configuration;
using AspNetCore.VersionInfo.Endpoint;
using AspNetCore.VersionInfo.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

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

            var endpointConventionBuilders = new List<IEndpointConventionBuilder>(new[] { apiEndpoint, uiEndpoint });
            return new VersionInfoConventionBuilder(endpointConventionBuilders);
        }
    }
}

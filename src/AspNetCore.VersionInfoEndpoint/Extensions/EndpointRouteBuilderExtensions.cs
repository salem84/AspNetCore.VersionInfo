using AspNetCore.VersionInfoEndpoint.Configuration;
using AspNetCore.VersionInfoEndpoint.Endpoint;
using AspNetCore.VersionInfoEndpoint.Middleware;
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
                    .UseMiddleware<VersionInfoApiEndpoint>()
                    .Build();

            var apiEndpoint = builder.Map(options.ApiPath, apiDelegate)
                .WithDisplayName("VersionInfo API");

            var uiDelegate =
                builder.CreateApplicationBuilder()
                    .UseMiddleware<UIMiddleware>()
                    .Build();


            var uiEndpoint = builder.Map(options.UIPath, uiDelegate)
                .WithDisplayName("VersionInfo UI");

            var endpointConventionBuilders = new List<IEndpointConventionBuilder>(new[] { apiEndpoint, uiEndpoint });
            return new VersionInfoConventionBuilder(endpointConventionBuilders);
        }
    }
}

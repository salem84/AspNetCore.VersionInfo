using AspNetCore.VersionInfo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Middleware
{
    class BadgeEndpoint
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<BadgeEndpoint> Logger;

        public BadgeEndpoint(RequestDelegate next, IServiceScopeFactory serviceScopeFactory, ILogger<BadgeEndpoint> logger)
        {
            this._serviceScopeFactory = serviceScopeFactory;
            this.Logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Dictionary<string, string> versionInfo;
            string responseContent;

            // Read VersionInfoId to use as key in providers dictionary 
            // (it's never empty because of route configuration)
            var id = context.Request.RouteValues[Constants.BADGE_PARAM_VERSIONINFOID] as string;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var infoHandler = scope.ServiceProvider.GetService<IInfoCollector>();

                // Collect all data
                versionInfo = infoHandler.AggregateData();

                // Retrieve versionInfo data by QueryString key
                var found = versionInfo.TryGetValue(id, out string versionInfoValue);
                if (!found)
                {
                    Logger.LogWarning($"Badge Endpoint Error: {Messages.BADGE_KEY_NOT_FOUND} - {id}");
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return;
                }

                // Set color found in QueryString, otherwise set BADGE_DEFAULT_COLOR
                var color = context.Request.Query[Constants.BADGE_PARAM_COLOR];
                if (string.IsNullOrEmpty(color))
                {
                    color = Constants.BADGE_DEFAULT_COLOR;
                }

                // Set label found in QueryString, otherwise set as Key
                var label = context.Request.Query[Constants.BADGE_PARAM_LABEL];
                if(string.IsNullOrEmpty(label))
                {
                    label = id;
                }

                // Draw badge
                responseContent = BadgePainter.DrawSVG(label, versionInfoValue, color);
            }

            // Set ContentType as image/svg+xml
            context.Response.ContentType = Constants.DEFAULT_BADGE_RESPONSE_CONTENT_TYPE;

            await context.Response.WriteAsync(responseContent);
        }
    }
}

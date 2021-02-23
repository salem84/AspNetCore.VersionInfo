﻿using AspNetCore.VersionInfo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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

        public BadgeEndpoint(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            this._serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Dictionary<string, string> versionInfo;
            string responseContent;

            var id = context.Request.RouteValues[Constants.BADGE_PARAM_VERSIONINFOID] as string;
            if(string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(Constants.BADGE_PARAM_VERSIONINFOID, Messages.BADGE_VERSIONINFOID_EMPTY);
            }

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var infoHandler = scope.ServiceProvider.GetService<IInfoCollector>();
                var badgePainter = scope.ServiceProvider.GetService<BadgePainter>();

                versionInfo = infoHandler.AggregateData();

                var found = versionInfo.TryGetValue(id, out string versionInfoValue);
                if (!found)
                {
                    throw new ArgumentOutOfRangeException(Constants.BADGE_PARAM_VERSIONINFOID, Messages.BADGE_KEY_NOT_FOUND);
                }

                var color = context.Request.Query[Constants.BADGE_PARAM_COLOR];
                if (string.IsNullOrEmpty(color))
                {
                    color = Constants.BADGE_DEFAULT_COLOR;
                }

                var displayName = context.Request.Query[Constants.BADGE_PARAM_DISPLAYNAME];
                if(string.IsNullOrEmpty(displayName))
                {
                    displayName = id;
                }
                responseContent = badgePainter.DrawSVG(displayName, versionInfoValue, color);
            }

            context.Response.ContentType = Constants.DEFAULT_BADGE_RESPONSE_CONTENT_TYPE;

            await context.Response.WriteAsync(responseContent);
        }
    }
}
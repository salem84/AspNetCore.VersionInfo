using AspNetCore.VersionInfo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Middleware
{
    internal class ApiEndpoint
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ApiEndpoint(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            this._serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Dictionary<string, string> versionInfo = null;
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var infoHandler = scope.ServiceProvider.GetService<IInfoHandler>();
                versionInfo = infoHandler.GetData();
            }

            var responseContent = JsonSerializer.Serialize(versionInfo ?? new Dictionary<string, string>());
            context.Response.ContentType = Constants.DEFAULT_API_RESPONSE_CONTENT_TYPE;

            await context.Response.WriteAsync(responseContent);
        }
    }
}

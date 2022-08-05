using System.Text.Json;
using System.Threading.Tasks;
using AspNetCore.VersionInfo.Models.Collectors;
using AspNetCore.VersionInfo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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
            string responseContent = string.Empty;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var infoHandler = scope.ServiceProvider.GetService<IInfoCollector>();
                var versionInfo = infoHandler.AggregateData() as FlatCollectorResult;
                responseContent = JsonSerializer.Serialize(versionInfo);
            }

            context.Response.ContentType = Constants.DEFAULT_API_RESPONSE_CONTENT_TYPE;

            await context.Response.WriteAsync(responseContent);
        }
    }
}

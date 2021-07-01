using AspNetCore.VersionInfo.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Middleware
{
    class HtmlEndpoint
    {
        private const string EmbeddedFileNamespace = "AspNetCore.VersionInfo.assets";

        private readonly VersionInfoOptions _options;
        private readonly StaticFileMiddleware _staticFileMiddleware;

        public HtmlEndpoint(
            RequestDelegate next,
            IWebHostEnvironment hostingEnv,
            ILoggerFactory loggerFactory,
            VersionInfoOptions options = null)
        {
            _options = options ?? new VersionInfoOptions();
            _staticFileMiddleware = CreateStaticFileMiddleware(next, hostingEnv, loggerFactory, _options);
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var httpMethod = httpContext.Request.Method;
            var page = httpContext.Request.RouteValues.FirstOrDefault().Value as string;

            if (httpMethod == "GET" && (string.IsNullOrEmpty(page) || page == "//"))
            {
                await RespondWithIndexHtml(httpContext.Response);

                return;
            }

            // Inserted for future use, to include also JS library in assembly
            await _staticFileMiddleware.Invoke(httpContext);
        }

        private StaticFileMiddleware CreateStaticFileMiddleware(
            RequestDelegate next,
            IWebHostEnvironment hostingEnv,
            ILoggerFactory loggerFactory,
            VersionInfoOptions options)
        {
            var staticFileOptions = new StaticFileOptions
            {
                RequestPath = string.IsNullOrEmpty(options.RoutePrefix) ? string.Empty : $"/{options.RoutePrefix}",
                FileProvider = new EmbeddedFileProvider(typeof(HtmlEndpoint).Assembly, EmbeddedFileNamespace),
            };

            return new StaticFileMiddleware(next, hostingEnv, Options.Create(staticFileOptions), loggerFactory);
        }

        private async Task RespondWithIndexHtml(HttpResponse response)
        {
            response.StatusCode = 200;
            response.ContentType = "text/html;charset=utf-8";

            string page = "indexGrouped";
            using (var stream = GetType().Assembly.GetManifestResourceStream($"{GetType().Assembly.GetName().Name}.assets.{page}.html")/*_options.IndexStream()*/)
            {
                var htmlBuilder = new StringBuilder(new StreamReader(stream).ReadToEnd());
                foreach (var entry in GetIndexArguments())
                {
                    htmlBuilder.Replace(entry.Key, entry.Value);
                }

                await response.WriteAsync(htmlBuilder.ToString(), Encoding.UTF8);
            }
        }

        private IDictionary<string, string> GetIndexArguments()
        {
            return new Dictionary<string, string>()
            {
                { "%(API_URL)%", _options.ApiPath }
            };
        }
    }
}


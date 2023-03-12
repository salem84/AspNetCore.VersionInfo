using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.VersionInfo.Configuration;
using AspNetCore.VersionInfo.Models;
using AspNetCore.VersionInfo.Models.Collectors;
using AspNetCore.VersionInfo.Models.Providers;
using AspNetCore.VersionInfo.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetCore.VersionInfo.Services
{
    internal partial class FlatInfoCollector : IInfoCollector
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IOptions<VersionInfoOptions> _options;
        private readonly ILogger<FlatInfoCollector> _logger;

        #region LoggerMessage
        [LoggerMessage(EventId = 1, Level = LogLevel.Debug, Message = "Elaborating {handlerName} provider")]
        private partial void LogElaboratingHandler(string handlerName);

        [LoggerMessage(EventId = 2, Level = LogLevel.Error, Message = "Error during elaboration of {handlerName} provider")]
        private partial void LogErrorElaborateHandler(string handlerName, Exception ex);
        #endregion

        public FlatInfoCollector(IServiceScopeFactory scopeFactory, IOptions<VersionInfoOptions> options, ILogger<FlatInfoCollector> logger)
        {
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<ICollectorResult> AggregateData(CancellationToken cancellationToken = default)
        {
            var result = new FlatCollectorResult();

            // Get only enabled providers 
            var providerRegistrations = _options.Value.Registrations.Where(p => p.Enabled);

            var tasks = new Task<InfoProviderResult>[providerRegistrations.Count()];
            var index = 0;
            foreach (var registration in providerRegistrations)
            {
                tasks[index++] = Task.Run(() => RunGetDataAsync(registration, cancellationToken), cancellationToken);
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);

            foreach (var task in tasks)
            {
                if (task.Result.Data != null)
                {
                    foreach (var d in task.Result.Data)
                    {
                        result.Add(new VersionDataProviderKeyValueResult()
                        {
                            Key = d.Key,
                            Value = d.Value,
                            ProviderName = task.Result.ProviderName
                        });
                    }
                }
            }

            return result;
        }

        private async Task<InfoProviderResult> RunGetDataAsync(ProviderRegistration registration, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var scope = _scopeFactory.CreateAsyncScope();
            await using (scope.ConfigureAwait(false))
            {
                // Create provider instance
                var handler = scope.ServiceProvider.GetService(registration.ProviderType) as IInfoProvider;

                LogElaboratingHandler(handler.Name);

                try
                {
                    var data = await handler.GetDataAsync();
                    return data;

                }
                catch (Exception ex)
                {
                    LogErrorElaborateHandler(handler.Name, ex);
                }
            }

            return null;
        }
    }
}

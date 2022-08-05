using AspNetCore.VersionInfo.Models;
using AspNetCore.VersionInfo.Models.Collectors;
using AspNetCore.VersionInfo.Providers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AspNetCore.VersionInfo.Services
{
    class FlatInfoCollector : IInfoCollector
    {
        private readonly IEnumerable<IInfoProvider> _infoHandlers;
        private readonly ILogger<FlatInfoCollector> _logger;

        public FlatInfoCollector(IEnumerable<IInfoProvider> infoHandlers, ILogger<FlatInfoCollector> logger)
        {
            _infoHandlers = infoHandlers;
            _logger = logger;
        }
        public ICollectorResult AggregateData()
        {
            // It's maybe better pass this dictionary as argument for all handlers?
            var result = new FlatCollectorResult();
            foreach (var handler in _infoHandlers)
            {
                _logger.LogDebug($"Elaborating {handler.Name} provider");
                foreach (var d in handler.GetData())
                {

                    result.Add(new VersionDataProviderKeyValueResult()
                    {
                        Key = d.Key,
                        Value = d.Value,
                        ProviderName = handler.Name
                    });
                }
            }

            return result;
        }
    }
}

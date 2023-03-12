using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCore.VersionInfo.Models;
using AspNetCore.VersionInfo.Models.Collectors;
using AspNetCore.VersionInfo.Providers;
using Microsoft.Extensions.Logging;

namespace AspNetCore.VersionInfo.Services
{
    internal partial class FlatInfoCollector : IInfoCollector
    {
        private readonly IEnumerable<IInfoProvider> _infoHandlers;
        private readonly ILogger<FlatInfoCollector> _logger;

        #region LoggerMessage
        [LoggerMessage(EventId = 1, Level = LogLevel.Debug, Message = "Elaborating {handlerName} provider")]
        private partial void LogElaboratingHandler(string handlerName);

        [LoggerMessage(EventId = 2, Level = LogLevel.Error, Message = "Error during elaboration of {handlerName} provider")]
        private partial void LogErrorElaborateHandler(string handlerName, Exception ex);
        #endregion

        public FlatInfoCollector(IEnumerable<IInfoProvider> infoHandlers, ILogger<FlatInfoCollector> logger)
        {
            _infoHandlers = infoHandlers;
            _logger = logger;
        }
        public async Task<ICollectorResult> AggregateData()
        {
            // It's maybe better pass this dictionary as argument for all handlers?
            var result = new FlatCollectorResult();
            foreach (var handler in _infoHandlers)
            {
                LogElaboratingHandler(handler.Name);
                try
                {
                    var data = await handler.GetDataAsync();
                    if (data != null)
                    {
                        foreach (var d in data.Data)
                        {
                            result.Add(new VersionDataProviderKeyValueResult()
                            {
                                Key = d.Key,
                                Value = d.Value,
                                ProviderName = handler.Name
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    LogErrorElaborateHandler(handler.Name, ex);
                }
            }

            return result;
        }
    }
}

using AspNetCore.VersionInfo.Models.Collectors;
using AspNetCore.VersionInfo.Providers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Services
{
    class GroupedInfoCollector : IInfoCollector
    {
        private readonly IEnumerable<IInfoProvider> _infoHandlers;
        private readonly ILogger<GroupedInfoCollector> _logger;

        public GroupedInfoCollector(IEnumerable<IInfoProvider> infoHandlers, ILogger<GroupedInfoCollector> logger)
        {
            _infoHandlers = infoHandlers;
            _logger = logger;
        }
        public ICollectorResult AggregateData()
        {
            var result = new GroupedCollectorResult();
            foreach (var handler in _infoHandlers)
            {
                //foreach (var d in handler.GetData())
                //{
                //    if (data.ContainsKey(d.Key))
                //    {
                //        _logger.LogWarning(Messages.DUPLICATED_KEY, d.Key);
                //    }
                //    else
                //    {
                //        data.Add(d.Key, d.Value);
                //    }
                //}
            }

            return result;
        }
    }
}

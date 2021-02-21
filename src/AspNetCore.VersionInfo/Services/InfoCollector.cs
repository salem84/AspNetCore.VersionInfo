using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Services
{
    public interface IInfoCollector
    {
        Dictionary<string, string> AggregateData();
    }

    class InfoCollector : IInfoCollector
    {
        private readonly IEnumerable<IInfoHandler> _infoHandlers;
        private readonly ILogger<InfoCollector> _logger;

        public InfoCollector(IEnumerable<IInfoHandler> infoHandlers, ILogger<InfoCollector> logger)
        {
            _infoHandlers = infoHandlers;
            _logger = logger;
        }
        public Dictionary<string, string> AggregateData()
        {
            // It's maybe better pass this dictionary as argument for all handlers
            var data = new Dictionary<string, string>();
            foreach(var handler in _infoHandlers)
            {
                foreach(var d in handler.GetData())
                {
                    if (data.ContainsKey(d.Key))
                    {
                        _logger.LogWarning(Messages.DUPLICATED_KEY, d.Key);
                    }
                    else
                    {
                        data.Add(d.Key, d.Value);
                    }
                }
            }

            return data;
        }
    }
}

﻿using AspNetCore.VersionInfo.Providers;
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
    class InfoCollector : IInfoCollector
    {
        private readonly IEnumerable<IInfoProvider> _infoHandlers;
        private readonly ILogger<InfoCollector> _logger;

        public InfoCollector(IEnumerable<IInfoProvider> infoHandlers, ILogger<InfoCollector> logger)
        {
            _infoHandlers = infoHandlers;
            _logger = logger;
        }
        public dynamic AggregateData()
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

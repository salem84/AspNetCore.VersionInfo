using AspNetCore.VersionInfo.Models;
using AspNetCore.VersionInfo.Providers;
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
        ICollectorResult AggregateData();
    }
}

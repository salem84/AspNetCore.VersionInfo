using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Models.Collectors
{
    public interface ICollectorResult
    {
        bool TryGetValue(string id, out string versionInfoValue);
        int Count { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Models.Collectors
{
    class GroupedCollectorResult : ICollectorResult
    {
        public int Count => throw new NotImplementedException();

        public bool TryGetValue(string id, out string versionInfoValue)
        {
            throw new NotImplementedException();
        }
    }
}

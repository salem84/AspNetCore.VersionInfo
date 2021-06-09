using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Models
{
    class VersionDataResult
    {
        public IEnumerable<VersionDataProviderResult> Results { get; set; }
    }

    class VersionDataProviderResult
    {
        public string ProviderName { get; set; }
        public Dictionary<string,string> Data { get; set; }
    }

    class VersionDataFlatResult
    {
        public IEnumerable<VersionDataProviderKeyValueResult> Results { get; set; }
    }

    class VersionDataProviderKeyValueResult
    {
        public string ProviderName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

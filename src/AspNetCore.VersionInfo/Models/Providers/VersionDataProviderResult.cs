using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Models.Providers
{
    public class VersionDataProviderResult
    {
        public string ProviderName { get; set; }
        public IDictionary<string,string> Data { get; set; }
    }
}

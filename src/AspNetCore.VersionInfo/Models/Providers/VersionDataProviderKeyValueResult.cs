using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Models
{

  
    public class VersionDataProviderKeyValueResult
    {
        public string ProviderName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Configuration
{
    public class ExclusionSettings
    {
        public IEnumerable<string> Keys { get; set; }
    }
}

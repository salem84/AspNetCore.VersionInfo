using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Configuration
{
    public class ExclusionSettingsService : IExclusionSettings
    {
        public IEnumerable<string> keys { get; set; }
        public ExclusionSettingsService(List<String> keys)
        {
            this.keys = keys;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Configuration
{
    public interface IExclusionSettings
    {
        IEnumerable<string> keys { get; set; }
    }
}

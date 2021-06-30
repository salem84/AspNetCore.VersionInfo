using AspNetCore.VersionInfo.Models.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Models
{
    public class VersionDataResult
    {
        public IEnumerable<VersionDataProviderResult> Results { get; set; }
    }
}

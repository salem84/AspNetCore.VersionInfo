using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Providers
{
    public interface IInfoProvider
    {
        IDictionary<string, string> GetData();
    }
}

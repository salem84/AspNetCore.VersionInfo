using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Services
{
    public class AssemblyVersionHandler : IInfoHandler
    {
        public IDictionary<string, string> GetData()
        {
            var dict = new Dictionary<string, string>();

            dict.Add(Constants.KEY_ENTRY_ASSEMBLY_VERSION, Assembly.GetEntryAssembly().GetName().Version.ToString());

            return dict;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Services
{
    class ClrVersionHandler : IInfoHandler
    {
        public Dictionary<string, string> GetData()
        {
            var dict = new Dictionary<string, string>();
            dict.Add(Constants.KEY_RUNTIME_VERSION, RuntimeInformation.FrameworkDescription);
            dict.Add(Constants.KEY_DOTNET_VERSION, Environment.Version.ToString());

            return dict;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Services
{
    public class ClrVersionHandler : IInfoHandler
    {
        public IDictionary<string, string> GetData()
        {
            var dict = new Dictionary<string, string>();
            dict.Add(Constants.KEY_RUNTIMEINFORMATION_FRAMEWORKDESCRIPTION, RuntimeInformation.FrameworkDescription);
            dict.Add(Constants.KEY_RUNTIMEINFORMATION_OSDESCRIPTION, RuntimeInformation.OSDescription);
            dict.Add(Constants.KEY_RUNTIMEINFORMATION_OSARCHITECTURE, RuntimeInformation.OSArchitecture.ToString());
            dict.Add(Constants.KEY_RUNTIMEINFORMATION_PROCESSARCHITECTURE, RuntimeInformation.ProcessArchitecture.ToString());
            dict.Add(Constants.KEY_RUNTIMEINFORMATION_RUNTIMEIDENTIFIER, RuntimeInformation.RuntimeIdentifier);

            dict.Add(Constants.KEY_DOTNET_VERSION, Environment.Version.ToString());

            return dict;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AspNetCore.VersionInfo.Providers
{
    public class ClrVersionProvider : IInfoProvider
    {
        public virtual string Name => nameof(ClrVersionProvider);

        public virtual IDictionary<string, string> GetData()
        {
            var dict = new Dictionary<string, string>();
            dict.Add(Constants.KEY_RUNTIMEINFORMATION_FRAMEWORKDESCRIPTION, RuntimeInformation.FrameworkDescription);
            dict.Add(Constants.KEY_RUNTIMEINFORMATION_OSDESCRIPTION, RuntimeInformation.OSDescription);
            dict.Add(Constants.KEY_RUNTIMEINFORMATION_OSARCHITECTURE, RuntimeInformation.OSArchitecture.ToString());
            dict.Add(Constants.KEY_RUNTIMEINFORMATION_PROCESSARCHITECTURE, RuntimeInformation.ProcessArchitecture.ToString());
            dict.Add(Constants.KEY_RUNTIMEINFORMATION_RUNTIMEIDENTIFIER, RuntimeInformation.RuntimeIdentifier);

            dict.Add(Constants.KEY_ENVIRONMENT_VERSION, Environment.Version.ToString());

            return dict;
        }
    }
}

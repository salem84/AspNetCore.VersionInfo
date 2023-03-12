using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AspNetCore.VersionInfo.Models.Providers;

namespace AspNetCore.VersionInfo.Providers
{
    public class ClrVersionProvider : IInfoProvider
    {
        public virtual string Name => nameof(ClrVersionProvider);

        public virtual Task<InfoProviderResult> GetDataAsync()
        {
            var data = new InfoProviderResult(Name);
            data.Add(Constants.KEY_RUNTIMEINFORMATION_FRAMEWORKDESCRIPTION, RuntimeInformation.FrameworkDescription);
            data.Add(Constants.KEY_RUNTIMEINFORMATION_OSDESCRIPTION, RuntimeInformation.OSDescription);
            data.Add(Constants.KEY_RUNTIMEINFORMATION_OSARCHITECTURE, RuntimeInformation.OSArchitecture.ToString());
            data.Add(Constants.KEY_RUNTIMEINFORMATION_PROCESSARCHITECTURE, RuntimeInformation.ProcessArchitecture.ToString());
            data.Add(Constants.KEY_RUNTIMEINFORMATION_RUNTIMEIDENTIFIER, RuntimeInformation.RuntimeIdentifier);

            data.Add(Constants.KEY_ENVIRONMENT_VERSION, Environment.Version.ToString());

            return Task.FromResult(data);
        }
    }
}

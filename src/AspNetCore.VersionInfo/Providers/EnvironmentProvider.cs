using System;
using System.Threading.Tasks;
using AspNetCore.VersionInfo.Models.Providers;

namespace AspNetCore.VersionInfo.Providers
{
    public class EnvironmentProvider : IInfoProvider
    {
        public virtual string Name => nameof(EnvironmentProvider);

        public virtual Task<InfoProviderResult> GetDataAsync()
        {
            var data = new InfoProviderResult(Name);

            data.Add(Constants.KEY_ENVIRONMENT_UPTIME, TimeSpan.FromMilliseconds(Environment.TickCount).ToString());
            data.Add(Constants.KEY_ENVIRONMENT_OSVERSION, Environment.OSVersion.ToString());
            data.Add(Constants.KEY_ENVIRONMENT_ISOSWINDOWS, IsOnWindows().ToString());
            data.Add(Constants.KEY_ENVIRONMENT_IS64BITOPERATINGSYSTEM, Environment.Is64BitOperatingSystem.ToString());
            data.Add(Constants.KEY_ENVIRONMENT_IS64BITPROCESS, Environment.Is64BitProcess.ToString());
            data.Add(Constants.KEY_ENVIRONMENT_PROCESSORCOUNT, Environment.ProcessorCount.ToString());
            data.Add(Constants.KEY_ENVIRONMENT_MACHINENAME, Environment.MachineName);
            data.Add(Constants.KEY_ENVIRONMENT_SYSTEMDIRECTORY, Environment.SystemDirectory);
            data.Add(Constants.KEY_ENVIRONMENT_WORKINGDIRECTORY, Environment.CurrentDirectory);
            data.Add(Constants.KEY_ENVIRONMENT_COMMANDLINE, Environment.CommandLine);
            data.Add(Constants.KEY_ENVIRONMENT_VERSION, Environment.Version.ToString());

            return Task.FromResult(data);
        }

        public static bool IsOnWindows()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                    return true;
                default:
                    return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Providers
{
    public class EnvironmentProvider : IInfoProvider
    {
        public IDictionary<string, string> GetData()
        {
            var dict = new Dictionary<string, string>();

            dict.Add(Constants.KEY_ENVIRONMENT_UPTIME, TimeSpan.FromMilliseconds(Environment.TickCount).ToString());
            dict.Add(Constants.KEY_ENVIRONMENT_OSVERSION, Environment.OSVersion.ToString());
            dict.Add(Constants.KEY_ENVIRONMENT_ISOSWINDOWS, IsOnWindows().ToString());
            dict.Add(Constants.KEY_ENVIRONMENT_IS64BITOPERATINGSYSTEM, Environment.Is64BitOperatingSystem.ToString());
            dict.Add(Constants.KEY_ENVIRONMENT_IS64BITPROCESS, Environment.Is64BitProcess.ToString());
            dict.Add(Constants.KEY_ENVIRONMENT_PROCESSORCOUNT, Environment.ProcessorCount.ToString());
            dict.Add(Constants.KEY_ENVIRONMENT_MACHINENAME, Environment.MachineName);
            dict.Add(Constants.KEY_ENVIRONMENT_SYSTEMDIRECTORY, Environment.SystemDirectory);
            dict.Add(Constants.KEY_ENVIRONMENT_WORKINGDIRECTORY, Environment.CurrentDirectory);
            dict.Add(Constants.KEY_ENVIRONMENT_COMMANDLINE, Environment.CommandLine);
            dict.Add(Constants.KEY_ENVIRONMENT_VERSION, Environment.Version.ToString());

            return dict;
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

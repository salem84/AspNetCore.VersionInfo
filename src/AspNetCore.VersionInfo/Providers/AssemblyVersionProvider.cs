using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AspNetCore.VersionInfo.Providers
{
    public class AssemblyVersionProvider : IInfoProvider
    {
        public string Name => nameof(AssemblyVersionProvider);

        public IDictionary<string, string> GetData()
        {
            var dict = new Dictionary<string, string>();

            var entryAssembly = Assembly.GetEntryAssembly();
            var fi = FileVersionInfo.GetVersionInfo(entryAssembly.Location);

            dict.Add(Constants.KEY_ENTRY_ASSEMBLY_VERSION, entryAssembly.GetName().Version.ToString());
            dict.Add(Constants.KEY_ENTRY_ASSEMBLY_FULLNAME, entryAssembly.FullName);
            dict.Add(Constants.KEY_ENTRY_ASSEMBLY_LOCATION, entryAssembly.Location);
            dict.Add(Constants.KEY_ENTRY_ASSEMBLY_DIRECTORY_PATH, Path.GetDirectoryName(entryAssembly.Location));
            dict.Add(Constants.KEY_ENTRY_ASSEMBLY_FILE_VERSION, fi.FileVersion);
            dict.Add(Constants.KEY_ENTRY_ASSEMBLY_CLR_VERSION, entryAssembly.ImageRuntimeVersion);
            dict.Add(Constants.KEY_ENTRY_ASSEMBLY_CREATION_DATE, File.GetCreationTime(entryAssembly.Location).ToString());
            dict.Add(Constants.KEY_ENTRY_ASSEMBLY_LASTMODIFIED_DATE, File.GetLastWriteTime(entryAssembly.Location).ToString());

            return dict;
        }
    }
}

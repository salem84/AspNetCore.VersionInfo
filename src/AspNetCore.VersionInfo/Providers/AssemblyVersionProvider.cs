using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AspNetCore.VersionInfo.Models.Providers;

namespace AspNetCore.VersionInfo.Providers
{
    public class AssemblyVersionProvider : IInfoProvider
    {
        public virtual string Name => nameof(AssemblyVersionProvider);

        public virtual Task<InfoProviderResult> GetDataAsync()
        {
            var data = new InfoProviderResult(Name);

            var entryAssembly = Assembly.GetEntryAssembly();
            var fi = FileVersionInfo.GetVersionInfo(entryAssembly.Location);

            data.Add(Constants.KEY_ENTRY_ASSEMBLY_VERSION, entryAssembly.GetName().Version.ToString());
            data.Add(Constants.KEY_ENTRY_ASSEMBLY_FULLNAME, entryAssembly.FullName);
            data.Add(Constants.KEY_ENTRY_ASSEMBLY_LOCATION, entryAssembly.Location);
            data.Add(Constants.KEY_ENTRY_ASSEMBLY_DIRECTORY_PATH, Path.GetDirectoryName(entryAssembly.Location));
            data.Add(Constants.KEY_ENTRY_ASSEMBLY_FILE_VERSION, fi.FileVersion);
            data.Add(Constants.KEY_ENTRY_ASSEMBLY_CLR_VERSION, entryAssembly.ImageRuntimeVersion);
            data.Add(Constants.KEY_ENTRY_ASSEMBLY_CREATION_DATE, File.GetCreationTime(entryAssembly.Location).ToString());
            data.Add(Constants.KEY_ENTRY_ASSEMBLY_LASTMODIFIED_DATE, File.GetLastWriteTime(entryAssembly.Location).ToString());

            return Task.FromResult(data);
        }
    }
}

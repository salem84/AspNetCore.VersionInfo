using System.Collections.Generic;

namespace AspNetCore.VersionInfo.Providers
{
    public interface IInfoProvider
    {
        string Name { get; }
        IDictionary<string, string> GetData();
    }
}

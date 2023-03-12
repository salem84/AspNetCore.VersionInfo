using System.Threading.Tasks;
using AspNetCore.VersionInfo.Models.Providers;

namespace AspNetCore.VersionInfo.Providers
{
    public interface IInfoProvider
    {
        string Name { get; }
        Task<InfoProviderResult> GetDataAsync();
    }
}

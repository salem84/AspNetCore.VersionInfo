using System.Threading.Tasks;
using AspNetCore.VersionInfo.Models.Collectors;

namespace AspNetCore.VersionInfo.Services
{
    public interface IInfoCollector
    {
        Task<ICollectorResult> AggregateData();
    }
}

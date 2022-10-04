using AspNetCore.VersionInfo.Models.Collectors;

namespace AspNetCore.VersionInfo.Services
{
    public interface IInfoCollector
    {
        ICollectorResult AggregateData();
    }
}

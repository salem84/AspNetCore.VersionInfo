namespace AspNetCore.VersionInfo.Models.Collectors
{
    public interface ICollectorResult
    {
        bool TryGetValue(string id, out string versionInfoValue);
        int Count { get; }
    }
}

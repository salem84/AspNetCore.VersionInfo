using System.Collections.Generic;
using System.Collections.Immutable;

namespace AspNetCore.VersionInfo.Models.Providers
{
    public class InfoProviderResult
    {
        public string ProviderName { get; init; }

        private readonly Dictionary<string, string> _data = new Dictionary<string, string>();
        public IImmutableDictionary<string, string> Data => _data.ToImmutableDictionary();

        public int Count => _data.Count;

        public InfoProviderResult(string providerName)
        {
            ProviderName = providerName;
        }
        public InfoProviderResult(string providerName, Dictionary<string, string> data)
        {
            ProviderName = providerName;
            _data = data;
        }

        public void Add(string key, string value)
        {
            _data.Add(key, value);
        }
    }
}

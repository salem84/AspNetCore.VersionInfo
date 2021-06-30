using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Models.Collectors
{
    public class FlatCollectorResult : ICollectorResult
    {
        public IList<VersionDataProviderKeyValueResult> Results { get; set; }
        public int Count => Results.Count;

        public FlatCollectorResult()
        {
            Results = new List<VersionDataProviderKeyValueResult>();
        }
        internal void Add(VersionDataProviderKeyValueResult versionDataProviderResult)
        {
            Results.Add(versionDataProviderResult);
        }

        public bool TryGetValue(string id, out string versionInfoValue)
        {
            var res = Results.SingleOrDefault(x => x.Key == id);
            if(res == null)
            {
                versionInfoValue = null;
                return false;
            }

            versionInfoValue = res.Value;
            return true;
        }

        public Dictionary<string, string> ToDictionary(bool includeProviderName)
        {
            if(includeProviderName)
            {
                return Results.ToDictionary(x => $"{x.ProviderName}:{x.Key}", x => x.Value);
            }
            else
            {
                return Results.ToDictionary(x => x.Key, x => x.Value);
            }
        }
    }
}

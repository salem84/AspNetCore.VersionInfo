using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Models
{
    public class VersionDataResult
    {
        public IEnumerable<VersionDataProviderResult> Results { get; set; }
    }

    public class VersionDataProviderResult
    {
        public string ProviderName { get; set; }
        public IDictionary<string,string> Data { get; set; }
    }

  
    public class VersionDataProviderKeyValueResult
    {
        public string ProviderName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }


    public interface ICollectorResult
    {
        bool TryGetValue(string id, out string versionInfoValue);
        int Count { get; }
        Dictionary<string, string> ToDictionary();
    }


    public class FlatCollectorResult : ICollectorResult
    {
        private IList<VersionDataProviderKeyValueResult> Results { get; set; }
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

        public Dictionary<string, string> ToDictionary()
        {
            return Results.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}

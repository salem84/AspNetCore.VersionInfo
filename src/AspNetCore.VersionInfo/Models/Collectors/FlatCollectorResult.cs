using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Models.Collectors
{
    public class FlatCollectorResult : ICollectorResult
    {
        // TODO make it private?
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
            // Check id is valid
            if(string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            VersionDataProviderKeyValueResult res = null;

            // Check if id is composed as ProviderName:Key
            var splitted = id.Split(Constants.PROVIDERNAME_SEPARATOR);
            if(splitted.Length > 1)
            {
                res = Results.SingleOrDefault(x => x.ProviderName == splitted[0] && x.Key == splitted[1]);
            }
            else
            {
                res = Results.SingleOrDefault(x => x.Key == splitted[0]);
            }

            // Value not found
            if (res == null)
            {
                versionInfoValue = null;
                return false;
            }

            // Return value
            versionInfoValue = res.Value;
            return true;
        }

        public Dictionary<string, string> ToDictionary(bool includeProviderName)
        {
            if(includeProviderName)
            {
                return Results.ToDictionary(x => x.ProviderName + Constants.PROVIDERNAME_SEPARATOR + x.Key, x => x.Value);
            }
            else
            {
                return Results.ToDictionary(x => x.Key, x => x.Value);
            }
        }
    }
}

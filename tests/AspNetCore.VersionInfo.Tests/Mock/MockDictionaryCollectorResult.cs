using System.Collections.Generic;
using AspNetCore.VersionInfo.Models.Collectors;

namespace AspNetCore.VersionInfo.Tests.Mock
{
    class MockDictionaryCollectorResult : ICollectorResult/*, IEnumerable<KeyValuePair<string, string>>*/
    {
        private Dictionary<string, string> Results { get; set; }

        public int Count => Results.Count;

        public Dictionary<string, string> ToDictionary() => Results;

        public bool TryGetValue(string id, out string versionInfoValue) => Results.TryGetValue(id, out versionInfoValue);

        //public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => Results.GetEnumerator();

        //IEnumerator IEnumerable.GetEnumerator() => Results.GetEnumerator();

        //public MockDictionaryCollectorResult(IEnumerable<KeyValuePair<string, string>> collection)
        //{
        //    Results = collection;
        //}
        public MockDictionaryCollectorResult(IDictionary<string, string> dictionary)
        {
            Results = new Dictionary<string, string>(dictionary);
        }

        public MockDictionaryCollectorResult()
        {
            Results = new Dictionary<string, string>();
        }
    }
}

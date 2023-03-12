using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.VersionInfo.Models.Providers;

namespace AspNetCore.VersionInfo.Providers
{
    public class AppDomainAssembliesVersionProvider : IInfoProvider
    {
        public virtual string Name => nameof(AppDomainAssembliesVersionProvider);

        public virtual Task<InfoProviderResult> GetDataAsync()
        {
            var dict = new SortedDictionary<string, string>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (!dict.ContainsKey(assembly.GetName().Name))
                {
                    dict.Add(assembly.GetName().Name, assembly.GetName().Version.ToString());
                }
            }

            var ret = dict.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            return Task.FromResult(new InfoProviderResult(Name, ret));
        }
    }
}

using System;
using System.Collections;
using System.Threading.Tasks;
using AspNetCore.VersionInfo.Models.Providers;

namespace AspNetCore.VersionInfo.Providers
{
    public class EnvironmentVariablesProvider : IInfoProvider
    {
        public virtual string Name => nameof(EnvironmentVariablesProvider);

        public virtual Task<InfoProviderResult> GetDataAsync()
        {
            var data = new InfoProviderResult(Name);

            foreach (DictionaryEntry envVar in Environment.GetEnvironmentVariables())
            {
                data.Add(envVar.Key.ToString(), envVar.Value.ToString());
            }

            return Task.FromResult(data);
        }
    }
}

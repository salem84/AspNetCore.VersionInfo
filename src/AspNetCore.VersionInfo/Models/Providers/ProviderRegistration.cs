using System;
using AspNetCore.VersionInfo.Configuration;

namespace AspNetCore.VersionInfo.Models.Providers
{
    public class ProviderRegistration
    {
        public Type ProviderType { get; set; }
        public bool Enabled { get; set; }
        public ProviderOptions Options { get; set; }
    }
}

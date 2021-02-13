using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfoEndpoint.Configuration
{
    public class VersionInfoOptions
    {
        public string UIPath { get; set; } = "/version";
        public string ApiPath { get; set; } = "/version-api";
    }
}

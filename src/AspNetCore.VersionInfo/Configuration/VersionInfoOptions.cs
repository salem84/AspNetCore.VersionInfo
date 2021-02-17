using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Configuration
{
    public class VersionInfoOptions
    {
        public string UIPath { get; set; } = "/version/{id}";
        public string ApiPath { get; set; } = "/version-api";
        public string RoutePrefix { get; set; } = "";
    }
}

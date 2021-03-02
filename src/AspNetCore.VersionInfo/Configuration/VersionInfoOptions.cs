using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Configuration
{
    public class VersionInfoOptions
    {
        public string HtmlPath { get; set; } = Constants.DEFAULT_HTML_ENDPOINT_URL;
        public string ApiPath { get; set; } = Constants.DEFAULT_API_ENDPOINT_URL;
        public string BadgePath { get; set; } = Constants.DEFAULT_BADGE_ENDPOINT_URL;
        internal string RoutePrefix { get; set; } = "";
    }
}

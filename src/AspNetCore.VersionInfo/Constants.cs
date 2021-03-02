using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo
{
    public static class Constants
    {
        internal const string DEFAULT_API_RESPONSE_CONTENT_TYPE = "application/json";
        internal const string DEFAULT_BADGE_RESPONSE_CONTENT_TYPE = "image/svg+xml";

        //public const string DEFAULT_HTML_ENDPOINT_URL = "/version/html/{id?}";
        public const string DEFAULT_HTML_ENDPOINT_URL = "/version/html";
        public const string DEFAULT_API_ENDPOINT_URL = "/version/json";
        public const string DEFAULT_BADGE_ENDPOINT_URL = "/version/badge/{versionInfoId}";

        public const string KEY_ENTRY_ASSEMBLY_VERSION = "EntryAssemblyVersion";
        public const string KEY_RUNTIME_VERSION = "RuntimeVersion";
        public const string KEY_DOTNET_VERSION = "DotNetVersion";

        public const string KEY_RUNTIMEINFORMATION_FRAMEWORKDESCRIPTION = "RuntimeInformation.FrameworkDescription";
        public const string KEY_RUNTIMEINFORMATION_OSDESCRIPTION = "RuntimeInformation.OsDescription";
        public const string KEY_RUNTIMEINFORMATION_OSARCHITECTURE = "RuntimeInformation.OsArchitecture";
        public const string KEY_RUNTIMEINFORMATION_PROCESSARCHITECTURE = "RuntimeInformation.ProcessArchitecture";
        public const string KEY_RUNTIMEINFORMATION_RUNTIMEIDENTIFIER = "RuntimeInformation.RuntimeIdentifier";

        public const string BADGE_PARAM_VERSIONINFOID = "versionInfoId";
        public const string BADGE_PARAM_LABEL = "label";
        public const string BADGE_PARAM_COLOR = "color";

        public const string BADGE_DEFAULT_COLOR = "Green";

    }

    public static class Messages
    {
        public const string DUPLICATED_KEY = "Duplicated key: {0}";
        public const string BADGE_KEY_NOT_FOUND = "Key not found";
        public const string BADGE_VERSIONINFOID_EMPTY = "VersionInfoId not valid in url";
    }
}

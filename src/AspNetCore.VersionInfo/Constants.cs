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

        public const string PROVIDERNAME_SEPARATOR = ":";

        public const string KEY_ENTRY_ASSEMBLY_VERSION = "EntryAssemblyVersion";
        public const string KEY_ENTRY_ASSEMBLY_FULLNAME = "EntryAssemblyFullName";
        public const string KEY_ENTRY_ASSEMBLY_LOCATION = "EntryAssemblyLocation";
        public const string KEY_ENTRY_ASSEMBLY_DIRECTORY_PATH = "EntryAssemblyDirectoryPath";
        public const string KEY_ENTRY_ASSEMBLY_FILE_VERSION = "EntryAssemblyFileVersion";
        public const string KEY_ENTRY_ASSEMBLY_CLR_VERSION = "EntryAssemblyClrVersion";
        public const string KEY_ENTRY_ASSEMBLY_CREATION_DATE = "EntryAssemblyCreationDate";
        public const string KEY_ENTRY_ASSEMBLY_LASTMODIFIED_DATE = "EntryAssemblyLastModifiedDate";

        public const string KEY_RUNTIMEINFORMATION_FRAMEWORKDESCRIPTION = "RuntimeInformation.FrameworkDescription";
        public const string KEY_RUNTIMEINFORMATION_OSDESCRIPTION = "RuntimeInformation.OsDescription";
        public const string KEY_RUNTIMEINFORMATION_OSARCHITECTURE = "RuntimeInformation.OsArchitecture";
        public const string KEY_RUNTIMEINFORMATION_PROCESSARCHITECTURE = "RuntimeInformation.ProcessArchitecture";
        public const string KEY_RUNTIMEINFORMATION_RUNTIMEIDENTIFIER = "RuntimeInformation.RuntimeIdentifier";

        public const string KEY_ENVIRONMENT_UPTIME = "Environment.Uptime";
        public const string KEY_ENVIRONMENT_OSVERSION = "Environment.OSVersion";
        public const string KEY_ENVIRONMENT_ISOSWINDOWS = "Environment.IsOsWindows";
        public const string KEY_ENVIRONMENT_IS64BITOPERATINGSYSTEM = "Environment.Is64BitOperatingSystem";
        public const string KEY_ENVIRONMENT_IS64BITPROCESS = "Environment.Is64BitProcess";
        public const string KEY_ENVIRONMENT_PROCESSORCOUNT = "Environment.ProcessorCount";
        public const string KEY_ENVIRONMENT_MACHINENAME = "Environment.MachineName";
        public const string KEY_ENVIRONMENT_SYSTEMDIRECTORY = "Environment.SystemDirectory";
        public const string KEY_ENVIRONMENT_WORKINGDIRECTORY = "Environment.WorkingDirectory";
        public const string KEY_ENVIRONMENT_COMMANDLINE = "Environment.CommandLine";
        public const string KEY_ENVIRONMENT_VERSION = "Environment.DotNetVersion";

        public const string BADGE_PARAM_VERSIONINFOID = "versionInfoId";
        public const string BADGE_PARAM_LABEL = "label";
        public const string BADGE_PARAM_COLOR = "color";

        public const string BADGE_DEFAULT_COLOR = "Green";
    }
}

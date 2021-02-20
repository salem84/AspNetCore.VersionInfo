using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Services
{
    public interface IInfoHandler
    {
        Dictionary<string, string> GetData();
    }

    class InfoHandler : IInfoHandler
    {
        public Dictionary<string, string> GetData()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("RuntimeVersion", RuntimeInformation.FrameworkDescription);
            dict.Add("NetVersion", Environment.Version.ToString());
            dict.Add("AssemblyVersion", Assembly.GetEntryAssembly().GetName().Version.ToString());

            return dict;
               
        }
    }
}

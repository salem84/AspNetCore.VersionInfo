using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfoEndpoint.Services
{
    class InfoHandler
    {
        public Dictionary<string, string> GetData()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("ClrVersion", "1.0");
            dict.Add("NetVersion", "5.0");
            dict.Add("AssemblyVersion", "1.0.0");

            return dict;
               
        }
    }
}

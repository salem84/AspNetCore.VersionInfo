using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Services
{
    public interface IInfoHandler
    {
        IDictionary<string, string> GetData();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Services
{
    public interface IInfoCollector
    {
        Dictionary<string, string> AggregateData();
    }

    class InfoCollector : IInfoCollector
    {
        private readonly IEnumerable<IInfoHandler> infoHandlers;

        public InfoCollector(IEnumerable<IInfoHandler> infoHandlers)
        {
            this.infoHandlers = infoHandlers;
        }
        public Dictionary<string, string> AggregateData()
        {
            var data = new Dictionary<string, string>();
            foreach(var handler in infoHandlers)
            {
                foreach(var d in handler.GetData())
                {
                    // TODO check duplicates
                    data.Add(d.Key, d.Value);
                }
            }

            return data;
        }
    }
}

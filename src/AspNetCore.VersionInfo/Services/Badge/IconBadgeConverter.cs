using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.VersionInfo.Services.Badge
{
    internal interface IIconBadgeConverter
    {
        string ConvertToSvgBase64(byte[] svgBytes);
    }


    /// <summary>
    /// Convert SVG to Base64 image
    /// </summary>
    internal class IconBadgeConverter : IIconBadgeConverter
    {
        public string ConvertToSvgBase64(byte[] svgBytes)
        {
            // Convert the byte array to a Base64 string with MIME header
            var base64 = "data:image/svg+xml;base64," + Convert.ToBase64String(svgBytes); 
            return base64;
        }

        public string ConvertToSvgBase64(string svgContent)
        {
            byte[] svgBytes = Encoding.UTF8.GetBytes(svgContent);
            return ConvertToSvgBase64(svgBytes);
        }
    }
}


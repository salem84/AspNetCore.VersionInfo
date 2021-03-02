using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Based on https://github.com/rebornix/DotBadge

namespace AspNetCore.VersionInfo.Services
{
    public interface IBadgePainter
    {
        string Draw(string subject, string status, string statusColor, Style style);
    }


    public enum Style
    {
        Flat,
        FlatSquare,
        Plastic
    }

    public static class ColorScheme
    {
        public const string BrightGreen = "#4c1";
        public const string Green = "#97CA00";
        public const string Yellow = "#dfb317";
        public const string YellowGreen = "#a4a61d";
        public const string Orange = "#fe7d37";
        public const string Red = "#e05d44";
        public const string Blue = "#007ec6";
        public const string Gray = "#555";
        public const string LightGray = "#9f9f9f";
    }

    public static class Resources
    {
        /// <summary>
        /// The flat 2.
        /// </summary>
        public const string Flat = @"<svg xmlns=""http://www.w3.org/2000/svg"" width=""{0}"" height=""20""><linearGradient id=""b"" x2=""0"" y2=""100%""><stop offset=""0"" stop-color=""#bbb"" stop-opacity="".1""/><stop offset=""1"" stop-opacity="".1""/></linearGradient><mask id=""a""><rect width=""{0}"" height=""20"" rx=""3"" fill=""#fff""/></mask><g mask=""url(#a)""><path fill=""#555"" d=""M0 0h{1}v20H0z""/><path fill=""{7}"" d=""M{1} 0h{2}v20H{1}z""/><path fill=""url(#b)"" d=""M0 0h{0}v20H0z""/></g><g fill=""#fff"" text-anchor=""middle"" font-family=""DejaVu Sans,Verdana,Geneva,sans-serif"" font-size=""11""><text x=""{3}"" y=""15"" fill=""#010101"" fill-opacity="".3"">{5}</text><text x=""{3}"" y=""14"">{5}</text><text x=""{4}"" y=""15"" fill=""#010101"" fill-opacity="".3"">{6}</text><text x=""{4}"" y=""14"">{6}</text></g></svg>";

        public const string FlatSquare = @"<svg xmlns=""http://www.w3.org/2000/svg"" width=""{0}"" height=""20""><g shape-rendering=""crispEdges""><path fill=""#555"" d=""M0 0h{1}v20H0z""/><path fill=""{7}"" d=""M{1} 0h{2}v20H{1}z""/></g><g fill=""#fff"" text-anchor=""middle"" font-family=""DejaVu Sans,Verdana,Geneva,sans-serif"" font-size=""11""><text x=""{3}"" y=""14"">{5}</text><text x=""{4}"" y=""14"">{6}</text></g></svg>";

        public const string Plastic = @"<svg xmlns=""http://www.w3.org/2000/svg"" width=""{0}"" height=""18""><linearGradient id=""b"" x2=""0"" y2=""100%""><stop offset=""0"" stop-color=""#fff"" stop-opacity="".7""/><stop offset="".1"" stop-color=""#aaa"" stop-opacity="".1""/><stop offset="".9"" stop-opacity="".3""/><stop offset=""1"" stop-opacity="".5""/></linearGradient><mask id=""a""><rect width=""{0}"" height=""18"" rx=""4"" fill=""#fff""/></mask><g mask=""url(#a)""><path fill=""#555"" d=""M0 0h{1}v18H0z""/><path fill=""{7}"" d=""M{1} 0h{2}v18H{1}z""/><path fill=""url(#b)"" d=""M0 0h{0}v18H0z""/></g><g fill=""#fff"" text-anchor=""middle"" font-family=""DejaVu Sans,Verdana,Geneva,sans-serif"" font-size=""11""><text x=""{3}"" y=""14"" fill=""#010101"" fill-opacity="".3"">{5}</text><text x=""{3}"" y=""13"">{5}</text><text x=""{4}"" y=""14"" fill=""#010101"" fill-opacity="".3"">{6}</text><text x=""{4}"" y=""13"">{6}</text></g></svg>";
    }

    public class BadgePainter : IBadgePainter
    {
        public string Draw(string subject, string status, string statusColor, Style style)
        {
            string template;
            string color;
            switch (style)
            {
                case Style.Flat:
                    template = Resources.Flat;
                    break;
                case Style.FlatSquare:
                    template = Resources.FlatSquare;
                    break;
                case Style.Plastic:
                    template = Resources.Plastic;
                    break;
                default:
                    throw new ArgumentException("Style not supported", nameof(style));
            }

            Font font = new Font("DejaVu Sans,Verdana,Geneva,sans-serif", 11, FontStyle.Regular);
            Graphics g = Graphics.FromImage(new Bitmap(1, 1));
            var subjectWidth = g.MeasureString(subject, font).Width;
            var statusWidth = g.MeasureString(status, font).Width;

            color = ParseColor(statusColor);

            var result = string.Format(
                CultureInfo.InvariantCulture,
                template,
                subjectWidth + statusWidth,
                subjectWidth,
                statusWidth,
                subjectWidth / 2 + 1,
                subjectWidth + statusWidth / 2 - 1,
                subject,
                status,
                color);
            return result;
        }

        private static string ParseColor(string input)
        {
            var type = typeof(ColorScheme);
            var fieldInfo = type.GetField(input);
            if (fieldInfo == null)
            {
                return String.Empty;
            }
            return (string)fieldInfo.GetValue(type);
        }
    }
}

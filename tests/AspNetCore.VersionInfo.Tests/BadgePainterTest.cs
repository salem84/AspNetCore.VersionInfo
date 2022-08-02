using AspNetCore.VersionInfo.Middleware;
using AspNetCore.VersionInfo.Services;
using AspNetCore.VersionInfo.Tests.Mock;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCore.VersionInfo.Tests
{
    public class BadgePainterTest : BaseIocTest
    {
        [Fact]
        public void DrawBadge()
        {
            // Arrange
            var painter = new BadgePainter();

            // Act
            var badge = painter.Draw("Framework", ".NET 6.0.0", Constants.BADGE_DEFAULT_COLOR, Style.Flat);

            // Assert
            Assert.StartsWith("<svg", badge);
        }

    }
}

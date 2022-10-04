using AspNetCore.VersionInfo.Services;
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

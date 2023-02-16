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
            var badge = painter.Draw("Framework", ".NET 6.0.0", Constants.BADGE_DEFAULT_COLOR, Style.Flat, null);

            // Assert
            Assert.StartsWith("<svg", badge);
        }

        [Fact]
        public void DrawBadge_WithoutIcon()
        {
            // Arrange
            var painter = new BadgePainter();

            // Act
            var badge = painter.Draw("Framework", ".NET 6.0.0", Constants.BADGE_DEFAULT_COLOR, Style.Flat, null);

            // Assert
            Assert.DoesNotContain("<icon", badge);
        }

        [Fact]
        public void DrawBadge_WithIcon()
        {
            // Arrange
            var painter = new BadgePainter();

            // Act
            var badge = painter.Draw("Framework", ".NET 6.0.0", Constants.BADGE_DEFAULT_COLOR, Style.Flat, "github");

            // Assert
            Assert.Contains("<image", badge);
            Assert.Contains("https://cdn.simpleicons.org/github/white", badge);

        }

        [Fact]
        public void DrawBadge_WithCustomColor()
        {
            // Arrange
            var painter = new BadgePainter();

            // Act
            var badge = painter.Draw("Framework", ".NET 6.0.0", Constants.BADGE_DEFAULT_COLOR, Style.Flat, null);

            // Assert
            Assert.StartsWith("<svg", badge);
        }
    }
}

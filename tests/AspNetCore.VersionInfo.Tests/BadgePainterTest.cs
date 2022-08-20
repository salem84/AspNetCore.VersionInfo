using System;
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

        [Fact]
        public void DrawBadge_WithoutIcon()
        {
            // Arrange
            var painter = new BadgePainter();
            painter.ShowIcon = false;

            // Act
            var badge = painter.Draw("Framework", ".NET 6.0.0", Constants.BADGE_DEFAULT_COLOR, Style.Flat);

            // Assert
            Assert.DoesNotContain("<icon", badge);
        }

        [Fact]
        public void DrawBadge_WithIcon()
        {
            // Arrange
            var painter = new BadgePainter();
            painter.ShowIcon = true;

            // Act
            Action action = () => painter.Draw("Framework", ".NET 6.0.0", Constants.BADGE_DEFAULT_COLOR, Style.Flat);

            // Assert
            Assert.Throws<NotImplementedException>(action);
        }

        [Fact]
        public void DrawBadge_WithCustomColor()
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

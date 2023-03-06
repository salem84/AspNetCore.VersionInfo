using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AspNetCore.VersionInfo.Services.Badge;
using Moq;
using Xunit;

namespace AspNetCore.VersionInfo.Tests.Services
{
    public class BadgePainterTest
    {
        [Theory]
        [InlineData("Framework", ".NET 6.0.0")]
        [InlineData("Framework", "")]
        [InlineData("", "")]
        [InlineData("UnicodeChars", "¥Щ")]
        [InlineData("CustomChar", "⌨")]
        public async Task DrawBadge(string subject, string status)
        {
            // Arrange
            var iconBadgeGenerator = new Mock<IIconBadgeGenerator>();
            var painter = new BadgePainter(iconBadgeGenerator.Object);
            var badgeInfo = new BadgeInfo()
            {
                Subject = subject,
                Status = status,
                StatusColor = Constants.BADGE_DEFAULT_COLOR,
                Style = Style.Flat
            };
            var regexSubject = new Regex(
            pattern: $"<text x=\"\\d+\" y=\"\\d+\" textLength=\"\\d+\">{subject}<\\/text>",
            options: RegexOptions.IgnoreCase,
            matchTimeout: TimeSpan.FromMilliseconds(200));

            var regexStatus = new Regex(
            pattern: $"<text x=\"\\d+\" y=\"\\d+\" textLength=\"\\d+\">{status}<\\/text>",
            options: RegexOptions.IgnoreCase,
            matchTimeout: TimeSpan.FromMilliseconds(200));

            // Act
            var badge = await painter.Draw(badgeInfo);

            // Assert
            Assert.StartsWith("<svg", badge);
            Assert.Matches(regexSubject, badge);
            Assert.Matches(regexStatus, badge);
        }

        [Fact]
        public async Task DrawBadge_WithoutIcon()
        {
            // Arrange
            var iconBadgeGenerator = new Mock<IIconBadgeGenerator>();
            var painter = new BadgePainter(iconBadgeGenerator.Object);
            var badgeInfo = new BadgeInfo()
            {
                Subject = "Framework",
                Status = ".NET 6.0.0",
                StatusColor = Constants.BADGE_DEFAULT_COLOR,
                Style = Style.Flat
            };

            // Act
            var badge = await painter.Draw(badgeInfo);

            // Assert
            Assert.StartsWith("<svg", badge);
            Assert.DoesNotContain("<icon", badge);
        }

        [Fact]
        public async Task DrawBadge_WithIcon()
        {
            // Arrange
            var iconBadgeGenerator = new Mock<IIconBadgeGenerator>();
            var painter = new BadgePainter(iconBadgeGenerator.Object);
            var badgeInfo = new BadgeInfo()
            {
                Subject = "Framework",
                Status = ".NET 6.0.0",
                StatusColor = Constants.BADGE_DEFAULT_COLOR,
                Style = Style.Flat,
                IconSlug = "github"
            };

            // Act
            var badge = await painter.Draw(badgeInfo);

            // Assert
            Assert.StartsWith("<svg", badge);
            Assert.Contains("<image", badge);
        }

        [Fact]
        public async Task DrawBadge_WithDefaultColor()
        {
            // Arrange
            var iconBadgeGenerator = new Mock<IIconBadgeGenerator>();
            var painter = new BadgePainter(iconBadgeGenerator.Object);
            var badgeInfo = new BadgeInfo()
            {
                Subject = "Framework",
                Status = ".NET 6.0.0",
                StatusColor = Constants.BADGE_DEFAULT_COLOR, // Default is Green
                Style = Style.Flat
            };
            var defaultColor = ColorScheme.Green;

            // Act
            var badge = await painter.Draw(badgeInfo);

            // Assert
            Assert.StartsWith("<svg", badge);
            Assert.Contains($"fill=\"{defaultColor}\"", badge);
        }

        [Fact]
        public async Task DrawBadge_WithSupportedColor()
        {
            // Arrange
            var iconBadgeGenerator = new Mock<IIconBadgeGenerator>();
            var painter = new BadgePainter(iconBadgeGenerator.Object);
            var badgeInfo = new BadgeInfo()
            {
                Subject = "Framework",
                Status = ".NET 6.0.0",
                StatusColor = ColorScheme.Blue,
                Style = Style.Flat
            };

            // Act
            var badge = await painter.Draw(badgeInfo);

            // Assert
            Assert.StartsWith("<svg", badge);
            Assert.Contains($"fill=\"{ColorScheme.Blue}\"", badge);
        }

        [Theory]
        [InlineData("#FF0000")]
        [InlineData("#FFF")]
        [InlineData("rgb(255,0,24)")]
        [InlineData("rgb(255, 0, 24)")]
        [InlineData("rgba(255, 0, 24, .5)")]
        [InlineData("hsla(170, 23%, 25%, 0.2)")]
        public async Task DrawBadge_WithValidColor(string color)
        {
            // Arrange
            var iconBadgeGenerator = new Mock<IIconBadgeGenerator>();
            var painter = new BadgePainter(iconBadgeGenerator.Object);
            var badgeInfo = new BadgeInfo()
            {
                Subject = "Framework",
                Status = ".NET 6.0.0",
                StatusColor = color,
                Style = Style.Flat
            };

            // Act
            var badge = await painter.Draw(badgeInfo);

            // Assert
            Assert.StartsWith("<svg", badge);
            Assert.Contains($"fill=\"{color}\"", badge);
        }

        [Theory]
        [InlineData("notvalidcolor")]
        [InlineData("#f2ewq")]
        public async Task DrawBadge_WithCustomNotValidColor(string notValidColor)
        {
            // Arrange
            var iconBadgeGenerator = new Mock<IIconBadgeGenerator>();
            var painter = new BadgePainter(iconBadgeGenerator.Object);
            var badgeInfo = new BadgeInfo()
            {
                Subject = "Framework",
                Status = ".NET 6.0.0",
                StatusColor = notValidColor,
                Style = Style.Flat
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => painter.Draw(badgeInfo));
        }
    }
}

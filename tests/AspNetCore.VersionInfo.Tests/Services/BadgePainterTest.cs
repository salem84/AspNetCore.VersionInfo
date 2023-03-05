using System.Threading.Tasks;
using AspNetCore.VersionInfo.Services.Badge;
using Moq;
using Xunit;

namespace AspNetCore.VersionInfo.Tests.Services
{
    public class BadgePainterTest : BaseIocTest
    {
        [Fact]
        public async Task DrawBadge()
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

        [Fact]
        public async Task DrawBadge_WithCustomColor()
        {
            // Arrange
            var iconBadgeGenerator = new Mock<IIconBadgeGenerator>();
            var painter = new BadgePainter(iconBadgeGenerator.Object);
            var badgeInfo = new BadgeInfo()
            {
                Subject = "Framework",
                Status = ".NET 6.0.0",
                StatusColor = "customcolor",
                Style = Style.Flat
            };

            // Act
            var badge = await painter.Draw(badgeInfo);

            // Assert
            Assert.StartsWith("<svg", badge);
            Assert.Contains("fill=\"customcolor\"", badge);
        }
    }
}

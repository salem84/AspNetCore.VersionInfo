using System.Threading.Tasks;
using AspNetCore.VersionInfo.Services.Badge;
using Moq;
using Xunit;

namespace AspNetCore.VersionInfo.Tests
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
            var iconBadgeGenerator = new Mock<IconBadgeGenerator>();
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
            Assert.DoesNotContain("<icon", badge);
        }

        [Fact]
        public async Task DrawBadge_WithIcon()
        {
            // Arrange
            var iconBadgeGenerator = new Mock<IconBadgeGenerator>();
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
            Assert.Contains("<image", badge);
        }

        [Fact]
        public async Task DrawBadge_WithCustomColor()
        {
            // Arrange
            var iconBadgeGenerator = new Mock<IconBadgeGenerator>();
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
    }
}

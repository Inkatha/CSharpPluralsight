using Xunit;
using DemoCode.Middleware;

namespace DemoCode.Tests.MiddlewareTests
{
    public class AssertingNullsBools
    {
        [Fact]
        public void ShouldHaveDefaultRandomGeneratedName()
        {
            var sut = new PlayerCharacter();

            Assert.False(string.IsNullOrWhiteSpace(sut.Name));
        }

        [Fact]
        public void ShouldHaveNicknameThatIsNull()
        {
            var sut = new PlayerCharacter();

            Assert.Null(sut.NickName);
        }

        [Fact]
        public void ShouldBeNoob()
        {
            var sut = new PlayerCharacter();

            Assert.True(sut.IsNoob);
        }
    }
}
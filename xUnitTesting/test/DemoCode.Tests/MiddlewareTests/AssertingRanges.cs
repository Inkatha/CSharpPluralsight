using Xunit;
using DemoCode.Middleware;

namespace DemoCode.Tests.MiddlewareTests
{
    public class AssertingRanges
    {
        [Fact]
        public void ShouldIncreaseHealthAfterSleep()
        {
            var sut = new PlayerCharacter { Health = 100 };

            sut.Sleep();

            Assert.InRange(sut.Health, 101, 200);
        }
    }
}
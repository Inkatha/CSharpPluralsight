using Xunit;
using DemoCode.Middleware;

namespace DemoCode.Tests.MiddlewareTests
{
    public class CalculatorTests
    {
        [Fact]
        public void ShouldAddInts()
        {
            var sut = new Calculator();

            var result = sut.AddInt(1, 2);

            Assert.Equal(3, result);
        }

        [Fact]
        public void ShouldAddDoubles()
        {
            var sut = new Calculator();

            var result = sut.addDouble(3.00, 6.45);

            Assert.Equal(9.45, result);
        }

        [Fact]
        public void ShouldDivideInt()
        {
            var sut = new Calculator();

            var result = sut.Divide(10, 2);

            Assert.Equal(5, result);
        }
    }
}
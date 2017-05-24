using System;
using Xunit;
using DemoCode.Middleware;

namespace DemoCode.Tests.MiddlewareTests
{
    public class AssertingExceptions
    {
        [Fact]
        public void ShouldErrorWhenDivideByZero()
        {
            var sut = new Calculator();

            Assert.Throws<DivideByZeroException>(() => sut.Divide(100, 0));
        }

        [Fact]
        public void ShouldErrorArgumentOutOfRange()
        {
            var sut = new Calculator();

            ArgumentOutOfRangeException thrownException = 
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.Divide(201, 1));

            Assert.Equal("value", thrownException.ParamName);
        }
    }
}
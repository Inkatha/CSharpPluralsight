using Xunit;
using DemoCode.Middleware;

namespace DemoCode.Tests.MiddlewareTests
{
    public class AssertingStringResults
    {
        [Fact]
        public void ShouldAddFirstNameAndLastName()
        {
            var sut = new NameJoiner();

            var result = sut.Join("John", "Doe");

            Assert.Equal("JOHN DOE", result, ignoreCase: true);
        }

        [Fact]
        public void ShouldJoinNames_SubstringContents()
        {
            var sut = new NameJoiner();
            
            var result = sut.Join("John", "Doe");

            Assert.Contains("John", result);
        }

        [Fact]
        public void ShouldJoinNames_StartsWith()
        {
            var sut = new NameJoiner();

            var result = sut.Join("John", "Doe");

            Assert.StartsWith("John", result);
        }

        [Fact]
        public void ShouldJoinNames_EndsWith()
        {
            var sut = new NameJoiner();

            var result = sut.Join("John", "Doe");

            Assert.EndsWith("Doe", result);
        }

        [Fact]
        public void ShouldJoinNames_Regex()
        {
            var sut = new NameJoiner();

            var result = sut.Join("John", "Doe");

            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", result);
        }
    }
}
using Xunit;
using DemoCode.Middleware;
using System.Collections.Generic;

namespace DemoCode.Tests.MiddlewareTests
{
    public class AssertingCollections
    {
        [Fact]
        public void ShouldHaveNoEmptyDefaultWeapons()
        {
            var sut = new PlayerCharacter();

            Assert.All(sut.Weapons, weapon => Assert.False(string.IsNullOrWhiteSpace(weapon)));
        }

        [Fact]
        public void ShouldHaveALongBowInWeapons()
        {
            var sut = new PlayerCharacter();

            Assert.Contains("Long Bow", sut.Weapons);
        }

        [Fact]
        public void ShouldNotHaveStaffOfWonder()
        {
            var sut = new PlayerCharacter();

            Assert.DoesNotContain("Staff of Wonder", sut.Weapons);
        }

        [Fact]
        public void ShouldHaveAtleastOneKindOfSword()
        {
            var sut = new PlayerCharacter();

            Assert.Contains(sut.Weapons, weapon => weapon.Contains("Sword"));
        }

        [Fact]
        public void ShouldContainDefaultWeapons()
        {
            var sut = new PlayerCharacter();

            var defaultWeapons = new List<string>()
            {
                "Long Bow",
                "Short Bow",
                "Short Sword",
                //"Staff of Wonder"
            };

            Assert.Equal(sut.Weapons, defaultWeapons);
        }
    }
}
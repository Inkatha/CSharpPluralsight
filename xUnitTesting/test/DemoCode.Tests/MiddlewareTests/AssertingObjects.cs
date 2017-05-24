using DemoCode.Middleware;
using Xunit;

namespace DemoCode.Tests.MiddlewareTests
{
    public class AssertingObjectTypes
    {
        [Fact]
        public void ShouldCreateNormalEnemy_SimpleExample()
        {
            var sut = new EnemyFactory();

            object enemy = sut.Create(false);

            Assert.IsType(typeof(NormalEnemy), enemy);
        }

        [Fact]
        public void ShouldCreateNormalEnemy_Cast()
        {
            var sut = new EnemyFactory();

            object enemy = sut.Create(false);

            NormalEnemy normalEnemy = Assert.IsType<NormalEnemy>(enemy);

            Assert.Equal(10, normalEnemy.Power);
        }

        [Fact]
        public void ShouldCreateNormalEnemy_ExcludedDerivedTypes()
        {
            var sut = new EnemyFactory();

            object enemy = sut.Create(false);

            Assert.IsAssignableFrom(typeof(Enemy), enemy);
        }

        [Fact]
        public void ShouldCreateNormalEnemy_IncludeDerivedTypes_Cast()
        {
            var sut = new EnemyFactory();

            object enemy = sut.Create(false);

            Enemy normalEnemy = Assert.IsAssignableFrom<Enemy>(enemy);

            Assert.Equal("Default Name", normalEnemy.Name);
        }

        [Fact]
        public void ShouldCreateBossEnemy_SimpleExample()
        {
            var sut = new EnemyFactory();

            object enemy = sut.Create(true);

            Assert.IsType(typeof(BossEnemy), enemy);
        }

        [Fact]
        public void ShouldCreateBossEnemy_Cast()
        {
            var sut = new EnemyFactory();

            object enemy = sut.Create(true);

            BossEnemy bossEnemy = Assert.IsType<BossEnemy>(enemy);
        }
    }
}
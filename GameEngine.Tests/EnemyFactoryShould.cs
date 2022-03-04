using System;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    [Trait("Category", "Enemy")]
    public class EnemyFactoryShould
    {
        private readonly ITestOutputHelper _output;

        public EnemyFactoryShould(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void CreateNormalEnemyByDefault()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie");

            _output.WriteLine("Creating Boss Enemy");

            Assert.IsType<NormalEnemy>(enemy);
        }

        [Fact(Skip ="Don't need to run this")]
        public void CreateNormalEnemyByDefault_NotTypeExample()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie");

            Assert.IsNotType<DateTime>(enemy);
        }

        [Fact]
        public void CreateBossEnemy()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie King", true);

            Assert.IsType<BossEnemy>(enemy);
        }

        [Fact]
        public void CreateBossEnemy_CastReturnedTypeExample()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie King", true);

            // Assert and get cast result
            BossEnemy boss = Assert.IsType<BossEnemy>(enemy);

            // Additional asserts on typed object
            Assert.Equal("Zombie King", boss.Name);
        }

        [Fact]
        public void CreateBossEnemy_CastReturnedAssinableTypeExample()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie King", true);

            // Assert and get cast result
            //Assert.IsType<Enemy>(enemy); // It won't work. Because IsType gets the specific type

            Assert.IsAssignableFrom<Enemy>(enemy);
        }

        [Fact]
        public void CreateSeparateInstances()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy1 = sut.Create("Zombie");
            Enemy enemy2 = sut.Create("Zombie");

            Assert.NotSame(enemy1, enemy2);
        }

        [Fact]
        public void NotAllowNullName()
        {
            EnemyFactory sut = new EnemyFactory();
            
            //Assert.Throws<ArgumentNullException>(() => sut.Create(null));
            Assert.Throws<ArgumentNullException>("name", () => sut.Create(null));
        }

        [Fact]
        public void OnlyAllowKingOrQueenBossEnemies()
        {
            EnemyFactory sut = new EnemyFactory();

            EnemyCreationException ex =
                Assert.Throws<EnemyCreationException>(() => sut.Create("Zombie", true));

            Assert.Equal("Zombie", ex.RequestedEnemyName);
        }
    }
}

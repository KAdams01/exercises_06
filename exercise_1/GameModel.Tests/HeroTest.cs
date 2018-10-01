using System;
using Xunit;

namespace GameModel.Tests
{
    public class HeroTest
    {
        [Fact]
        public void IsAlive_HitPointsAreAboveZero_ShouldReturnTrue()
        {
            // Arrange
            var hero = new Hero{HitPoints = 1};
            // Act
            var result = hero.IsAlive();
            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(2, true)]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void isAlive_HitPointsVariable_ShouldReturnTrueIfGreaterThanZero(int value, bool expResult){
            // Arrange
            var hero = new Hero{HitPoints = value};
            // Act
            var result = hero.IsAlive();
            // Assert
            Assert.Equal(result, expResult);
        }

        [Fact]
        public void IsAlive_HitPointsAreZero_ShouldReturnFalse()
        {
            var hero = new Hero{HitPoints = 0};
            var result = hero.IsAlive();
            Assert.False(result);
        }
        
        [Fact]
        public void IsAlive_HitPointsAreBelowZero_ShouldReturnFalse()
        {
            var hero = new Hero{HitPoints = -1};
            var result = hero.IsAlive();
            Assert.False(result);
        }

        [Fact]
        public void Attack_NotAlive_ShouldReturnZero()
        {
            var hero = new Hero{HitPoints = 0};
            var attack = hero.Attack();
            Assert.Equal(0, attack);
        }

        [Fact]
        public void Attack_WithoutWeapon_ShouldReturnAttackValueFromStrengthOnly()
        {
            var hero = new Hero{HitPoints = 1, Strength = 5};
            int attackValue = hero.Attack();
            Assert.Equal(attackValue, 5);
        }

        [Fact]
        public void Attack_WithWeaponWithAttackOne_ShouldReturnAttackValue()
        {
            var hero = new Hero{HitPoints = 1, Strength = 6, MainHandWeapon = new Weapon{Attack = 1}};
            var result = hero.Attack();
            Assert.Equal(3, result);

        }

        [Fact]
        public void Attack_WithWeapon_ShouldReturnAttackValue()
        {
            var hero = new Hero{HitPoints = 1, Strength = 6, MainHandWeapon = new Weapon{Attack = 2}};
            var result = hero.Attack();
            Assert.Equal(6, result);
        }

        [Fact]
        public void Defend_WithDefenceZero_ShouldSubtractFromHitPoints()
        {
            var hero = new Hero{HitPoints = 10, Defence = 0};
            var opponent = new Hero{Strength = 10};
            hero.Defend(opponent);
            var resultHealth = hero.HitPoints;
            Assert.Equal(5, resultHealth);
        }

        [Fact]
        public void Defend_WithDefenceAboveZero_ShouldSubtractFromHitpoints()
        {
            var hero = new Hero{HitPoints = 10, Defence = 2};
            var opponent = new Hero{Strength = 10};
            hero.Defend(opponent);
            var resultHealth = hero.HitPoints;
            Assert.Equal(7, resultHealth);
        }

        [Fact]
        public void Defend_IsKilledFromAttack_HitPointsShouldNotGoBelowZero()
        {
            var hero = new Hero{HitPoints = 1, Defence = 2};
            var opponent = new Hero{Strength = 10};
            hero.Defend(opponent);
            var resultHealth = hero.HitPoints;
            Assert.Equal(0, resultHealth);
        }

        [Fact]
        public void Defend_OpponentIsNull_ShouldThrowArgumentNullException()
        {
            var hero = new Hero();
            var opponent = new Hero();
            opponent = null;
            Action action = () => hero.Defend(opponent);
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}

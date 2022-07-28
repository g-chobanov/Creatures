using GameCreatures.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GameCreatures.UnitTests.CreatureTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void SetProperties()
        {
            var creature = new Creature("Dummy", 10, 10, AttackType.Magic, ArmorType.Medium);

            Assert.AreEqual("Dummy", creature.Name);
            Assert.AreEqual(10, creature.Damage);
            Assert.AreEqual(10, creature.HealthPoints);
            Assert.AreEqual(AttackType.Magic, creature.AttackType);
            Assert.AreEqual(ArmorType.Medium, creature.ArmorType);
        }

        [TestMethod]
        public void ThrowArgumentNullException_NullName()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new Creature(null, 10, 10, default, default));
        }

        [TestMethod]
        public void ThrowRangeException_InvalidDamage()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => new Creature("", 0, 10, default, default));
        }

        [TestMethod]
        public void ThrowRangeException_InvalidHealth()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => new Creature("", 10, 0, default, default));
        }
    }
}

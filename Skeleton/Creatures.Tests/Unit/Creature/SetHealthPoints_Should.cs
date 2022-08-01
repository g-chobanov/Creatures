using Creatures.Services;
using GameCreatures.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameCreatures.UnitTests.CreatureTests
{
    [TestClass]
    public class SetHealthPoints_Should
    {
        private BestTargetFinder _finder = new BestTargetFinder();
        [TestMethod]
        public void CalculateCorrectly()
        {
            var creature = new Creature("Dummy", 10, 10, AttackType.Magic, ArmorType.Medium, _finder);
            creature.HealthPoints -= 3;

            Assert.AreEqual(7, creature.HealthPoints);
        }

        [TestMethod]
        public void SetToZero_IfDamageIsOverkill()
        {
            var creature = new Creature("Dummy", 10, 10, AttackType.Magic, ArmorType.Medium, _finder);
            creature.HealthPoints -= 11;

            Assert.AreEqual(0, creature.HealthPoints);
        }
    }
}

using GameCreatures.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameCreatures.UnitTests.CreatureTests
{
    [TestClass]
    public class CalculateActualDamage_Should
    {
        [TestMethod]
        public void DoNotDamageTarget()
        {
            var attacker = new Creature("Dummy", 10, 10, default, default);
            var target = new Creature("Dummy", 10, 10, default, default);

            attacker.CalculateActualDamage(target);

            Assert.AreEqual(10, target.HealthPoints);
        }

        /* ¯\_(ツ)_/¯ */
        [TestMethod]
        [DataRow(62, ArmorType.Light, AttackType.Ranged)]
        [DataRow(50, ArmorType.Medium, AttackType.Ranged)]
        [DataRow(37, ArmorType.Heavy, AttackType.Ranged)]
        [DataRow(50, ArmorType.Light, AttackType.Melee)]
        [DataRow(62, ArmorType.Medium, AttackType.Melee)]
        [DataRow(37, ArmorType.Heavy, AttackType.Melee)]
        [DataRow(37, ArmorType.Light, AttackType.Magic)]
        [DataRow(50, ArmorType.Medium, AttackType.Magic)]
        [DataRow(62, ArmorType.Heavy, AttackType.Magic)]
        public void CalculateCorrectDamage(
            int damage, ArmorType armorType, AttackType attackType)
        {
            var attacker = new Creature("Dummy", 50, 1000, attackType, default);
            var target = new Creature("Dummy", 1, 100, default, armorType);

            Assert.AreEqual(damage, attacker.CalculateActualDamage(target));
        }
    }
}

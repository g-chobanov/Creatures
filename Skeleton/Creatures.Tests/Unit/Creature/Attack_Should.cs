using GameCreatures.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameCreatures.UnitTests.CreatureTests
{
    [TestClass]
    public class Attack_Should
    {
        /* ¯\_(ツ)_/¯ */
        [TestMethod]
        [DataRow(63, ArmorType.Light, AttackType.Magic)]
        [DataRow(50, ArmorType.Medium, AttackType.Magic)]
        [DataRow(38, ArmorType.Heavy, AttackType.Magic)]
        [DataRow(38, ArmorType.Light, AttackType.Ranged)]
        [DataRow(50, ArmorType.Medium, AttackType.Ranged)]
        [DataRow(63, ArmorType.Heavy, AttackType.Ranged)]
        [DataRow(50, ArmorType.Light, AttackType.Melee)]
        [DataRow(38, ArmorType.Medium, AttackType.Melee)]
        [DataRow(63, ArmorType.Heavy, AttackType.Melee)]
        public void DealCorrectDamageToTarget(
            int expectedHealth, ArmorType armorType, AttackType attackType)
        {
            var attacker = new Creature("Dragon", 50, 1000, attackType, default);
            var target = new Creature("Sheep", 1, 100, default, armorType);

            attacker.Attack(target);

            Assert.AreEqual(expectedHealth, target.HealthPoints);
        }
    }
}

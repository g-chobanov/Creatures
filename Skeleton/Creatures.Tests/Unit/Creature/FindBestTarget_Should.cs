using GameCreatures.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameCreatures.UnitTests.CreatureTests
{
    [TestClass]
    public class FindBestTarget_Should
    {
        [TestMethod]
        public void DoNotDamageProposedTarget()
        {
            var target = new Creature("Dummy", 1, 10, default, default);
            var attacker = new Creature("Dummy", 1000, 1, default, default);

            var bestTarget = attacker
                .FindBestTarget(new List<Creature> { target });

            Assert.AreEqual(10, bestTarget.HealthPoints);
        }

        [TestMethod]
        public void FindBest_SingleItemList()
        {
            var target = new Creature("Dummy", 1, 1, default, default);
            var attacker = new Creature("Dummy", 1, 1, default, default);

            var bestTarget = attacker
                .FindBestTarget(new List<Creature> { target });

            Assert.AreSame(target, bestTarget);
        }

        [TestMethod]
        public void FindBest_EqualHealth_DifferentArmorTargets()
        {
            var target1 = new Creature("Dummy", 1, 100, default, ArmorType.Medium);
            var target2 = new Creature("Dummy", 1, 100, default, ArmorType.Heavy);

            var attacker = new Creature("Dummy", 10, 100, AttackType.Magic, default);

            var bestTarget = attacker
                .FindBestTarget(new List<Creature> { target1, target2 });
            
            // target1 (10 dmg, 90 health)
            // target2 (12 dmg, 88 health)
            Assert.AreSame(target2, bestTarget);
        }

        [TestMethod]
        public void FindBest_DifferentArmor_DifferentHealthTargets()
        {
            var target1 = new Creature("Dummy", 1, 80, default, ArmorType.Medium);
            var target2 = new Creature("Dummy", 1, 100, default, ArmorType.Heavy);

            var attacker = new Creature("Dummy", 20, 100, AttackType.Magic, default);

            var bestTarget = attacker
                .FindBestTarget(new List<Creature> { target1, target2 });

            // target1 (20 dmg, 60 health)
            // target2 (25 dmg, 75 health)
            Assert.AreSame(target1, bestTarget);
        }

        [TestMethod]
        public void FindBest_BonusDamage_OvercomesHealthDisadvantage()
        {
            var target1 = new Creature("Dummy", 1, 85, default, ArmorType.Medium);
            var target2 = new Creature("Dummy", 1, 90, default, ArmorType.Heavy);

            var attacker = new Creature("Dummy", 40, 100, AttackType.Magic, default);

            var bestTarget = attacker
                .FindBestTarget(new List<Creature> { target1, target2 });

            // target1 (40 dmg, 45 health)
            // target2 (48 dmg, 42 health)
            Assert.AreSame(target2, bestTarget);
        }

        [TestMethod]
        public void FindBest_BothTargetsWouldDie_PreferMoreDamagingOne()
        {
            var target1 = new Creature("Dummy", 10, 10, default, ArmorType.Medium);
            var target2 = new Creature("Dummy", 15, 15, default, ArmorType.Medium);

            var attacker = new Creature("Dummy", 35, 100, AttackType.Magic, default);

            var actualTarget = attacker
                .FindBestTarget(new List<Creature> { target1, target2 });

            // target1 (35 dmg taken, 0 health, 10 dmg)
            // target2 (35 dmg taken, 0 health, 15 dmg)
            Assert.AreSame(target2, actualTarget);
        }
    }
}

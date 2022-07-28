using GameCreatures.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameCreatures.UnitTests.CreatureTests
{
    [TestClass]
    public class AutoAttack_Should
    {
        [TestMethod]
        public void AttackBestTarget_SingleTarget()
        {
            var target = new Creature("Dummy", 1, 10, default, default);
            var attacker = new Creature("Dummy", 2, 1, default, default);

            attacker.AutoAttack(new List<Creature> { target });

            Assert.AreEqual(8, target.HealthPoints);
        }

        [TestMethod]
        public void AttackBestTarget_MultipleTargets()
        {
            var target1 = new Creature("Dummy", 1, 100, default, ArmorType.Medium);
            var target2 = new Creature("Dummy", 1, 100, default, ArmorType.Heavy);

            var attacker = new Creature("Dummy", 10, 100, AttackType.Magic, default);

            attacker.AutoAttack(new List<Creature> { target1, target2 });

            Assert.AreEqual(100, target1.HealthPoints);
            Assert.AreEqual(88, target2.HealthPoints); 
        }

        [TestMethod]
        public void AttackAndKill_MostDangerousTarget()
        {
            var target1 = new Creature("Dummy", 10, 10, default, ArmorType.Medium);
            var target2 = new Creature("Dummy", 15, 15, default, ArmorType.Medium);

            var attacker = new Creature("Dummy", 35, 100, AttackType.Magic, default);

            attacker.AutoAttack(new List<Creature> { target1, target2 });

            Assert.AreEqual(10, target1.HealthPoints);
            Assert.AreEqual(0, target2.HealthPoints);
        }
    }
}

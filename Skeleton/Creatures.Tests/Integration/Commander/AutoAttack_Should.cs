using GameCreatures.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameCreatures.IntegrationTests.CommanderTests
{
    [TestClass]
    public class AutoAttack_Should
    {
        [TestMethod]
        public void ChooseCorrectTarget()
        {
            // attacker1: 37 dmg to target1 (53 health remaining)
            // attacker1: 50 dmg to target2 (40 health remaining)
            // attacker2: 50 dmg to target1 (50 health remaining)
            // attacker2: 62 dmg to target2 (38 health remaining)
            // target2 should be best, although it starts with more health than target1

            var target1 = new Creature("Dummy", 10, 90, default, ArmorType.Light);
            var target2 = new Creature("Dummy", 10, 100, default, ArmorType.Medium);
            var attacker1 = new Creature("Dummy", 50, 100, AttackType.Magic, default);
            var attacker2 = new Creature("Dummy", 50, 100, AttackType.Melee, default);
            var commander1 = new Commander("Cmd1", new List<Creature> { attacker1, attacker2 });
            var commander2 = new Commander("Cmd2", new List<Creature> { target1, target2 });

            commander1.AutoAttack(commander2);

            Assert.AreEqual(38, target2.HealthPoints);
        }

        [TestMethod]
        public void ChooseBestAttacker_SingleTargetArmy()
        {
            // attacker1: 37 dmg to target1
            // attacker2: 50 dmg to target2
            var target1 = new Creature("Dummy", 10, 100, default, ArmorType.Light);
            var attacker1 = new Creature("Dummy", 50, 100, AttackType.Magic, default);
            var attacker2 = new Creature("Dummy", 50, 100, AttackType.Melee, default);
            var commander1 = new Commander("Cmd1", new List<Creature> { attacker1, attacker2 });
            var commander2 = new Commander("Cmd2", new List<Creature> { target1 });

            commander1.AutoAttack(commander2);

            Assert.AreEqual(50, target1.HealthPoints);
        }

        [TestMethod]
        public void ChooseTarget_ThatWouldDie()
        {
            // attacker1: 62 dmg to target1 (0 health remaining)
            // attacker1: 50 dmg to target2 (50 health remaining)
            // attacker2: 55 dmg to target1 (5 health remaining)
            // attacker2: 68 dmg to target2 (32 health remaining)

            // attacker2 does more damage to target2,
            // but attacker1 will kill target1

            var target1 = new Creature("Dummy", 10, 60, default, ArmorType.Light);
            var target2 = new Creature("Dummy", 10, 100, default, ArmorType.Medium);
            var attacker1 = new Creature("Dummy", 50, 100, AttackType.Ranged, default);
            var attacker2 = new Creature("Dummy", 55, 100, AttackType.Melee, default);
            var commander1 = new Commander("Cmd1", new List<Creature> { attacker1, attacker2 });
            var commander2 = new Commander("Cmd2", new List<Creature> { target1, target2 });

            commander1.AutoAttack(commander2);

            Assert.AreEqual(0, target1.HealthPoints);
        }

        [TestMethod]
        public void ReduceArmySize_WhenTargetIsKilled()
        {
            var commander1 = new Commander("Cmd1", 
                new List<Creature> { new Creature("Dummy", 10, 10, default, default) });
            var commander2 = new Commander("Cmd2", 
                new List<Creature> { new Creature("Dummy", 10, 10, default, default) });

            commander1.AutoAttack(commander2);

            Assert.AreEqual(0, commander2.ArmySize);
        }
    }
}

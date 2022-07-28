using GameCreatures.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameCreatures.IntegrationTests.CommanderTests
{
    [TestClass]
    public class AttackAtPosition_Should
    {
        [TestMethod]
        [DataRow(0, 63)]
        [DataRow(1, 50)]
        [DataRow(2, 38)]
        public void Attack_WithCorrectAttacker(int attackerIndex, int expectedHealth)
        {
            var target = new Creature("Dummy", 10, 100, default, ArmorType.Light);
            var attacker1 = new Creature("Dummy", 50, 100, AttackType.Magic, default);
            var attacker2 = new Creature("Dummy", 50, 100, AttackType.Melee, default);
            var attacker3 = new Creature("Dummy", 50, 100, AttackType.Ranged, default);
            var commander1 = new Commander("Cmd1", new List<Creature> { attacker1, attacker2, attacker3 });
            var commander2 = new Commander("Cmd2", new List<Creature> { target });

            commander1.AttackAtPosition(commander2, attackerIndex, 0);

            Assert.AreEqual(expectedHealth, target.HealthPoints);
        }

        [TestMethod]
        [DataRow(0, 38)]
        [DataRow(1, 50)]
        [DataRow(2, 63)]
        public void Attack_AtCorrectTarget(int targetIndex, int expectedHealth)
        {
            var targetList = new List<Creature>
            {
                new Creature("Dummy", 10, 100, default, ArmorType.Light),
                new Creature("Dummy", 10, 100, default, ArmorType.Medium),
                new Creature("Dummy", 10, 100, default, ArmorType.Heavy)
            };

            var attacker = new Creature("Dummy", 50, 100, AttackType.Ranged, default);
            var commander1 = new Commander("Cmd1", new List<Creature> { attacker });
            var commander2 = new Commander("Cmd2", targetList);

            commander1.AttackAtPosition(commander2, 0, targetIndex);

            Assert.AreEqual(expectedHealth, targetList[targetIndex].HealthPoints);
        }

        [TestMethod]
        public void ReduceArmySize_WhenTargetDies()
        {
            var commander1 = new Commander("Cmd1",
                new List<Creature> { new Creature("Dummy", 10, 10, default, default) });
            var commander2 = new Commander("Cmd2",
                new List<Creature> { new Creature("Dummy", 10, 10, default, default) });

            commander1.AttackAtPosition(commander2, 0, 0);

            Assert.AreEqual(0, commander2.ArmySize);
        }
    }
}

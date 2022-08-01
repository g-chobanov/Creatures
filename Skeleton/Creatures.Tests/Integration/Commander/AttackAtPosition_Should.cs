using GameCreatures.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Creatures.Services;

namespace GameCreatures.IntegrationTests.CommanderTests
{
    [TestClass]
    public class AttackAtPosition_Should
    {
        private BestTargetFinder _finder = new BestTargetFinder();
        [TestMethod]
        public void Attack_Attacker_Out_Of_Bound_Exception_Thrown()
        {
            var creature1 = new Creature("Dummy", 10, 100, default, default, _finder);
            var creature2 = new Creature("Dummy", 10, 100, default, default, _finder);
            var commander1 = new Commander("Cmd1", new List<Creature> { creature1 });
            var commander2 = new Commander("Cmd2", new List<Creature> { creature2 });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => commander1.AttackAtPosition(commander2, -1, 1));
        }
        public void Attack_Target_Out_Of_Bound_Exception_Thrown()
        {
            var creature1 = new Creature("Dummy", 10, 100, default, default, _finder);
            var creature2 = new Creature("Dummy", 10, 100, default, default, _finder);
            var commander1 = new Commander("Cmd1", new List<Creature> { creature1 });
            var commander2 = new Commander("Cmd2", new List<Creature> { creature2 });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => commander1.AttackAtPosition(commander2, 1, -1));
        }
        [TestMethod]
        [DataRow(0, 63)]
        [DataRow(1, 50)]
        [DataRow(2, 38)]
        public void Attack_WithCorrectAttacker(int attackerIndex, int expectedHealth)
        {
            var target = new Creature("Dummy", 10, 100, default, ArmorType.Light, _finder);
            var attacker1 = new Creature("Dummy", 50, 100, AttackType.Magic, default, _finder);
            var attacker2 = new Creature("Dummy", 50, 100, AttackType.Melee, default, _finder);
            var attacker3 = new Creature("Dummy", 50, 100, AttackType.Ranged, default, _finder);
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
                new Creature("Dummy", 10, 100, default, ArmorType.Light, _finder),
                new Creature("Dummy", 10, 100, default, ArmorType.Medium, _finder),
                new Creature("Dummy", 10, 100, default, ArmorType.Heavy, _finder)
            };

            var attacker = new Creature("Dummy", 50, 100, AttackType.Ranged, default, _finder);
            var commander1 = new Commander("Cmd1", new List<Creature> { attacker });
            var commander2 = new Commander("Cmd2", targetList);

            commander1.AttackAtPosition(commander2, 0, targetIndex);

            Assert.AreEqual(expectedHealth, targetList[targetIndex].HealthPoints);
        }

        [TestMethod]
        public void ReduceArmySize_WhenTargetDies()
        {
            var commander1 = new Commander("Cmd1",
                new List<Creature> { new Creature("Dummy", 10, 10, default, default, _finder) });
            var commander2 = new Commander("Cmd2",
                new List<Creature> { new Creature("Dummy", 10, 10, default, default, _finder) });

            commander1.AttackAtPosition(commander2, 0, 0);

            Assert.AreEqual(0, commander2.ArmySize);
        }
    }
}

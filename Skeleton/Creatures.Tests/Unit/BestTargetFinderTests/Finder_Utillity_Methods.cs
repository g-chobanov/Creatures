using GameCreatures.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using CreaturePair = System.Collections.Generic.KeyValuePair<int, GameCreatures.Models.Creature>;
using Creatures.Services;



namespace Creatures.Tests.Unit.BestTargetFinderTests
{
    [TestClass]
    public class Finder_Utillity_Methods
    {
        private BestTargetFinder _finder = new BestTargetFinder();

        [TestMethod]
        public void SortByMostDamaged_Should()
        {
            Creature target1 = new Creature("1", 10, 80, default, GameCreatures.ArmorType.Medium, _finder);
            Creature target2 = new Creature("2", 10, 5, default, GameCreatures.ArmorType.Light, _finder);
            Creature attacker = new Creature("attac", 1, 80, GameCreatures.AttackType.Ranged, default, _finder);

            List<Creature> damagedTargets = _finder.SortByMostDamaged(new List<Creature>() { target1, target2 }, attacker);

            //targets list should not be changed
            Assert.AreEqual(80, target1.HealthPoints);
            Assert.AreEqual(5, damagedTargets[0].HealthPoints);

        }

        [TestMethod]
        public void TakeDyingTargets_Should()
        {
            Creature target1 = new Creature("1", 10, 80, default, GameCreatures.ArmorType.Medium, _finder);
            Creature target2 = new Creature("2", 10, 5, default, GameCreatures.ArmorType.Light, _finder);
            //takes into account armour
            Creature target3 = new Creature("3", 10, 11, default, GameCreatures.ArmorType.Light, _finder);
            Creature attacker = new Creature("attac", 10, 80, GameCreatures.AttackType.Ranged, default, _finder);

            List<Creature> damagedTargets = _finder.TakeDyingTargets(new List<Creature>() { target1, target2, target3 }, attacker);

            Assert.AreEqual(damagedTargets.Count, 2);

        }

        [TestMethod]
        public void FindMostDangerous_Should()
        {
            Creature target1 = new Creature("1", 20, 80, default, GameCreatures.ArmorType.Medium, _finder);
            Creature target2 = new Creature("2", 10, 5, default, GameCreatures.ArmorType.Light, _finder);
            Creature target3 = new Creature("3", 10, 11, default, GameCreatures.ArmorType.Light, _finder);
            Creature attacker = new Creature("attac", 10, 80, GameCreatures.AttackType.Ranged, default, _finder);

            Creature dangerousTarget = _finder.FindMostDangerous(new List<Creature>() { target1, target2, target3 }, attacker);

            Assert.AreSame(dangerousTarget, target1);

        }

        [TestMethod]
        //takes into account damage amplifiers
        public void FindMostDangerous_Should_Amplifiers()
        {
            Creature target1 = new Creature("1", 10, 80, GameCreatures.AttackType.Ranged, default, _finder);
            Creature target2 = new Creature("2", 10, 5, GameCreatures.AttackType.Melee, default, _finder);
            
            Creature target3 = new Creature("3", 10, 11, GameCreatures.AttackType.Magic, default, _finder);
            Creature attacker = new Creature("attac", 10, 80, default, GameCreatures.ArmorType.Light, _finder);

            Creature dangerousTarget = _finder.FindMostDangerous(new List<Creature>() { target1, target2, target3 }, attacker);

            Assert.AreSame(dangerousTarget, target1);

        }

        [TestMethod]
        public void TakeLowestHPTargets()
        {
            Creature target1 = new Creature("1", 10, 40, default, GameCreatures.ArmorType.Medium, _finder);
            Creature target2 = new Creature("2", 10, 45, default, GameCreatures.ArmorType.Light, _finder);
            //takes into account armour
            Creature target3 = new Creature("3", 10, 40, default, GameCreatures.ArmorType.Light, _finder);
            Creature attacker = new Creature("attac", 10, 80, GameCreatures.AttackType.Ranged, default, _finder);

            List<Creature> damagedTargets = _finder.TakeLowestHPTargets(new List<Creature>() { target1, target2, target3 }, attacker);

            Assert.AreEqual(damagedTargets.Count, 1);

        }
        [TestMethod]
        public void FindBestDamageTarget_Should_One_Target()
        {
            Creature target1 = new Creature("1", 10, 80, GameCreatures.AttackType.Ranged, default, _finder);
            Creature attacker = new Creature("attac", 10, 80, default, GameCreatures.ArmorType.Light, _finder);

            Creature bestTarget = _finder.FindBestDamagedTarget(new List<Creature>() { target1 }, attacker);

            Assert.AreSame(bestTarget, target1);
        }

        [TestMethod]
        public void FindBestDamageTarget_Should_More_Than_One_Target()
        {
            Creature target1 = new Creature("1", 10, 80, GameCreatures.AttackType.Ranged, default, _finder);
            Creature target2 = new Creature("2", 10, 5, GameCreatures.AttackType.Melee, default, _finder);

            Creature target3 = new Creature("3", 10, 11, GameCreatures.AttackType.Magic, default, _finder);
            Creature attacker = new Creature("attac", 10, 80, default, GameCreatures.ArmorType.Light, _finder);

            Creature bestTarget = _finder.FindBestDamagedTarget(new List<Creature>() { target1, target2, target3 }, attacker);

            Assert.AreSame(bestTarget, target1);
        }

    }
}

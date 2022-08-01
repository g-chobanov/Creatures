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
        /*
        private BestTargetFinder _finder = new BestTargetFinder();
        [TestMethod]
        public void SimulateDamage_Should()
        {
            Creature target1 = new Creature("1", 10, 100, default, GameCreatures.ArmorType.Medium, _finder);
            Creature target2 = new Creature("2", 10, 80, default, GameCreatures.ArmorType.Light, _finder);
            Creature attacker = new Creature("attac", 10, 80, GameCreatures.AttackType.Ranged, default, _finder);

            List<CreaturePair> damagedTargets = _finder.SimulateDamage(new List<Creature>() { target1, target2 }, attacker);



        }
        */
    }
}

using GameCreatures.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GameCreatures.UnitTests.CommanderTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void SetProperties()
        {
            var commander = new Commander("cmd", 
                new List<Creature>() { new Creature("Dummy", 1, 1, default, default) });

            Assert.AreEqual("cmd", commander.Name);
            Assert.AreEqual(1, commander.ArmySize);
        }

        [TestMethod]
        public void ThrowException_NullName()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Commander(null, new List<Creature>()));
        }

        [TestMethod]
        public void ThrowException_NullList()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Commander("Hasname", null));
        }
    }
}

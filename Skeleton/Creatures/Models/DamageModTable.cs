using System;
using System.Collections.Generic;
using System.Text;
using GameCreatures;

namespace Creatures.Models
{
    static class DamageModTable
    {
        private const int AttackTypeCount = (int)AttackType.Magic + 1;
        private const int ArmorTypeCount = (int)ArmorType.Heavy + 1;

        public static readonly double[,] DMT = new double[AttackTypeCount, ArmorTypeCount] { {1.25, 1.00 ,0.75 },
                                                                                             {1.00, 1.25, 0.75 },
                                                                                             {0.75, 1.00, 1.25 } };
    }
}

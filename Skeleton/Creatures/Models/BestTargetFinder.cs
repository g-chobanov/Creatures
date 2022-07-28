using GameCreatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CreaturePair = System.Collections.Generic.KeyValuePair<int, GameCreatures.Models.Creature>;


namespace Creatures.Models
{
    public class BestTargetFinder
    {
        
        public Creature FindBestTarget(List<Creature> targets, Creature attacker)
        {
            if(targets == null)
            {
                throw new ArgumentNullException("There are no targets");
            }
            
            List<CreaturePair> damagedTargets = SimulateDamage(targets, attacker);
            List<CreaturePair> dyingTargets = TakeDyingTargets(damagedTargets);

            int bestDyingTargetIndex = FindBestDamagedTarget(dyingTargets, attacker);
            if (bestDyingTargetIndex != -1)
            {
                return targets[bestDyingTargetIndex];
            }

            List<CreaturePair> lowestHPTargets = TakeLowestHPTargets(damagedTargets);
            int bestDamagedTargetIndex = FindBestDamagedTarget(lowestHPTargets, attacker);
            return targets[bestDamagedTargetIndex];
        }

        private int FindMostDangerous(List<CreaturePair> targets, Creature attacker)
        {
            int maxDamage = 0;
            int index = 0;
            foreach(CreaturePair target in targets)
            {
                int currentTargetDamage = target.Value.CalculateActualDamage(attacker);
                if(currentTargetDamage > maxDamage)
                {
                    maxDamage = currentTargetDamage;
                    index = target.Key;
                }
            }
            return index;
        }

        private List<CreaturePair> SimulateDamage(List<Creature> targets, Creature attacker)
        {
            List<Creature> copyTargets = targets.ConvertAll(t => new Creature(t.Name,
                                                                              t.Damage,
                                                                              t.HealthPoints,
                                                                              t.AttackType,
                                                                              t.ArmorType));
            List<CreaturePair> simulatedTargets = new List<CreaturePair>();
            for(int i = 0; i < copyTargets.Count; i++)
            {
                copyTargets[i].TakeDamage(attacker.CalculateActualDamage(copyTargets[i]));
                simulatedTargets.Add(new CreaturePair(i, copyTargets[i]));
            }
            return simulatedTargets;
        }
        private List<CreaturePair> TakeDyingTargets(List<CreaturePair> targets)
        {
            return targets.FindAll(t => t.Value.HealthPoints == 0);
        }

        private List<CreaturePair> TakeLowestHPTargets(List<CreaturePair> targets)
        {
            int lowestHP = targets.Min(t => t.Value.HealthPoints);
            return targets.FindAll(t => t.Value.HealthPoints == lowestHP);
        }

        private int FindBestDamagedTarget(List<CreaturePair> targets, Creature attacker)
        {
            if ( targets.Count > 1)
            {
                return FindMostDangerous(targets, attacker);   
            }
            if ( targets.Count == 1)
            {
                return targets[0].Key;
            }
            return -1;
        }


    }
}

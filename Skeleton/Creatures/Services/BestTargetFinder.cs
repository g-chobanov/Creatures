using Creatures.Services.Contracts;
using GameCreatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Creatures.Services
{
    public class BestTargetFinder : IBestTargetFinder
    {

        public GameCreatures.Models.Creature FindBestTarget(List<GameCreatures.Models.Creature> targets, GameCreatures.Models.Creature attacker)
        {
            if (targets == null)
            {
                throw new ArgumentNullException("There are no targets");
            }


            List<Creature> damagedTargets = SortByMostDamaged(targets, attacker);
            List<Creature> dyingTargets = TakeDyingTargets(damagedTargets, attacker);
            Creature bestDyingTarget = FindBestDamagedTarget(dyingTargets, attacker);
            if (bestDyingTarget != null)
            {
                return bestDyingTarget;
            }

            List<Creature> lowestHPTargets = TakeLowestHPTargets(damagedTargets, attacker);
            Creature bestDamagedTarget = FindBestDamagedTarget(lowestHPTargets, attacker);
           
            return bestDamagedTarget;
        }

        public Creature FindMostDangerous(List<Creature> targets, Creature attacker)
        {
            int maxDamage = 0;
            int index = 0;
            for (int i = 0 ; i < targets.Count; i++)
            {
                int currentTargetDamage = targets[i].CalculateActualDamage(attacker);
                if (currentTargetDamage > maxDamage)
                {
                    maxDamage = currentTargetDamage;
                    index = i;
                }
            }
            return targets[index];
        }

        public List<Creature> SortByMostDamaged(List<Creature> targets, Creature attacker)
        {
            List<Creature> newTargets = targets.OrderBy(t => t.HealthPoints - attacker.CalculateActualDamage(t)).ToList();
            return newTargets;
        }
        public List<Creature> TakeDyingTargets(List<Creature> targets, Creature attacker)
        {
            return targets.FindAll(t => t.HealthPoints - attacker.CalculateActualDamage(t) <= 0);
        }

        public List<Creature> TakeLowestHPTargets(List<Creature> targets, Creature attacker)
        {
            int lowestHP = targets.Min(t => t.HealthPoints - attacker.CalculateActualDamage(t));
            return targets.FindAll(t => t.HealthPoints - attacker.CalculateActualDamage(t) == lowestHP);
        }

        public Creature FindBestDamagedTarget(List<Creature> targets, Creature attacker)
        {
            if (targets.Count > 1)
            {
                return FindMostDangerous(targets, attacker);
            }
            if (targets.Count == 1)
            {
                return targets[0];
            }
            return null;
        }


    }
}

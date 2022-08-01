using C5;
using System;
using System.Collections.Generic;

namespace GameCreatures.Models
{
    public class Commander 
    {
        private readonly List<Creature> _army;
        private string _name;

        public Commander(string name, List<Creature> army)
        {
            if(army == null)
            {
                throw new ArgumentNullException("Army can not be null!");
            }
            this.Name = name;
            this._army = army;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == null) 
                {
                    throw new ArgumentNullException("Name can not be null!"); 
                }
                _name = value;
            }
        }

        public int ArmySize
        {
            get => this._army.Count;
        }

        public void AttackAtPosition(Commander enemy, int attackerIndex, int targetIndex)
        {
            if ( attackerIndex < 0 || attackerIndex >= this._army.Count )
            {
                throw new ArgumentOutOfRangeException("Attacker or target index out of range!");
            }
            if (targetIndex < 0 || targetIndex >= enemy._army.Count)
            {
                throw new ArgumentOutOfRangeException("Attacker or target index out of range!");
            }
      
            Creature attacker = this._army[attackerIndex];
            Creature target = enemy._army[targetIndex];
            attacker.Attack(target);
            if (target.HealthPoints == 0)
            {
                enemy._army.Remove(target);
            }
        }

        public void AutoAttack(Commander enemy)
        {
            int indexOfBestTarget = 0;
            int lowestHP = int.MaxValue;

            List<Creature> bestTargets = this._army.ConvertAll(t => t.FindBestTarget(enemy._army));
            for (int i = 0; i < bestTargets.Count; i++)
            {
                int currentActualHealth = bestTargets[i].HealthPoints - this._army[i].CalculateActualDamage(bestTargets[i]);
                if (currentActualHealth <= 0)
                {
                    this.AttackAtPosition(enemy, i, enemy._army.IndexOf(bestTargets[i]));
                    return;
                }
                if (currentActualHealth < lowestHP)
                {
                    lowestHP = currentActualHealth;
                    indexOfBestTarget = i;
                }
            }

            this.AttackAtPosition(enemy, indexOfBestTarget, enemy._army.IndexOf(bestTargets[indexOfBestTarget]));
        }
    }
}

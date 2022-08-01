using System;
using System.Collections.Generic;
using Creatures.Models;
using Creatures.Contracts;
using Creatures.Services;
using Creatures.Services.Contracts;

namespace GameCreatures.Models
{

    public class Creature : IDamagable
    {
        private string _name;
        private int _damage;
        private int _healthPoints;
        private IBestTargetFinder _targetFinder;

        public Creature(
            string name,
            int damage,
            int hitPoints,
            AttackType attackType,
            ArmorType armorType,
            IBestTargetFinder targetFinder)
        {
            if (hitPoints <= 0)
            {
                throw new ArgumentOutOfRangeException("Health points should be initially positibe");
            }
            this.Name = name;
            this.Damage = damage;
            this.HealthPoints = hitPoints;
            this.AttackType = attackType;
            this.ArmorType = armorType;
            _targetFinder = targetFinder;

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
                this._name = value;
            } 
        }

        public int Damage 
        {
            get => this._damage;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Damage should always be positive.");
                }
                this._damage = value;
            }
        }
        
        public int HealthPoints
        {
            get => _healthPoints;
            set
            {
                if (value < 0)
                {
                    _healthPoints = 0;
                }
                else
                {
                    _healthPoints = value;
                }
            }
        }

        public ArmorType ArmorType { get; }

        public AttackType AttackType { get; }

        public void Attack(Creature target)
        {
            int modifiedDamage = this.CalculateActualDamage(target);
            target.TakeDamage(modifiedDamage);
        }

        //leaving it as it is for project purposes, would remove it in other cases and just call targetFinder's methods
        public Creature FindBestTarget(List<Creature> targets)
        {
            return _targetFinder.FindBestTarget(targets, this);
        }

        public void AutoAttack(List<Creature> targets)
        {
            this.Attack(this.FindBestTarget(targets));
        }

        public int CalculateActualDamage(Creature target)
        {
            double amplifier = DamageModTable.DMT[(int)this.AttackType, (int)target.ArmorType];
            int modifiedDamage = Convert.ToInt32(Math.Floor(amplifier * this.Damage));
            return modifiedDamage;
        }

        public void TakeDamage(int damage)
        {
            this.HealthPoints -= damage;
        }
    }
}

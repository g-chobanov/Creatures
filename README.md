# Creatures

## Task

The template (skeleton) contains two projects with automated tests. Your task is to make those tests pass by implementing the missing functionality.

## Constraints
- You are only to write in the **Creature** and **Commander** class.
- You are not allowed to modify the existing tests.
- You can add new methods/properties, but **cannot remove** existing ones.

## Guidelines

### Unit Tests

Unit tests are used for testing the state or behavior of a single _unit_. In Object Oriented Programming this unit is the **class**.

### Integration Tests

Integration tests are considered to be more complex than unit tests because they test how **units** (classes) interact with each other. 

For example, some methods of the **Commander** class rely on methods provided by the **Creature** class.  

#### Getting Started

First, try to make the Unit Tests pass and _then_ move to the Integration Tests.

All unit tests can be found in the `Creatures.Tests.Unit` project and all integration tests are in the `Creatures.Tests.Integration` project respectivelly.

---

## Units (Classes)

### Creature

This class represents a creature that has health, damage, attack and armor types, can attack, can decide which target is most vulnerable, and can calculate the effectiveness of its attack.

**Name**: _string_, should never be null.  
**Damage**: _int_, should always be positive.  
**HealthPoints**: _int_, initial(passed to constructor) HealthPoints should always be positive. HealthPoints have **public setter**, which keeps the HealthPoints at least at 0. (e.g. initial HealthPoints: 10, attack for 20 damage happens, HealthPoints are set to 0 instead of -10)  
**AttackType**: _enum_ (Ranged, Melee, Magic)  
**ArmorType**: _enum_ (Light, Medium, Heavy)  

<hr>

**CalculateActualDamage(Creature target)**: _int_  
Calculates the actual damage that will be done to the target. 
Each AttackType is strong vs some ArmorTypes and weak versus other ArmorTypes. The coefficients are described in this table:

|| Light &nbsp;| Medium &nbsp;&nbsp;| Heavy &nbsp;&nbsp;|
|---|---|---|---|
|**Ranged**&nbsp;&nbsp;|1.25|1.00|0.75|
|**Melee**|1.00|1.25|0.75|
|**Magic**|0.75|1.00|1.25|

The modified damage is **always rounded down**.

**Examples:**  
**Attacker(Magic Attack, 50 dmg)** attacks **Target(Heavy Armor)**:
Magic has bonus vs Heavy, so damage is calculated to 50 * 1.25 = 62.5. This is rounded down to 62.    

**Attacker(Magic Attack, 50 dmg)** attacks **Target(Light Armor)**:
Magic is reduced vs Light, so damage is calculated to 50 * 0.75 = 37.5. This is rounded down to 37.

<hr>

**Attack(Creature target)**: _void_   
Deals damage equal to the attacker **Damage**, (modified by Attack/Armor bonuses) to the target Creature.  
**Examples:**  
**Attacker(Magic Attack, 50 dmg)** attacks **Target(Heavy Armor, 100 health)**  
Magic has bonus vs Heavy, so damage is calculated to 50 * 1.25 = 62.5. This is rounded down to 62. Target health is reduced to 100 - 62 = 38.  

**Attacker(Magic Attack, 50 dmg)** attacks **Target(Light Armor, 100)**  
Magic is reduced vs Light, so damage is calculated to 50 * 0.75 = 37.5. This is rounded down to 37. Target health is reduced to 100 - 37 = 63.

<hr>

**FindBestTarget(List\<Creature\> targets)**: _Creature_  
Returns the Creature that is most optimal to be attacked:
1. If a creature will 'die' by the attack (health reduced to 0). This will be the best target. If two or more creatures will die, return the one that does most damage (the most 'dangerous' one).
2. If no creature will die, return the one that will be closest to death after the attack (that will have the least amount of health). Again, if two or more creatures will have equal amount of health, return the one that does most damage.

Check the tests in `FindBestTarget_Should.cs` for more examples.

<hr>

**AutoAttack(List\<Creature\> targets)**:) _void_  
Finds the best target from the list of creatures and performs an Attack. Don't forget to take into account Attack/Armor modifiers!

Check the tests in `AutoAttack_Should.cs` in the **Creature** folder to get a better understanding.

<br><br>

### Commander
This class represents a commander that has an army of creatures. They can do battles with other commanders and their armies.

**Constructor(string name, List\<Creature\> army)** - guards against null arguments.

**Name**: _string_, should never be null.  
**ArmySize**: _int_, this is the number of creatures in the commander's army.  

<hr>

**AttackAtPosition(Commander enemy, int attackerIndex, int targetIndex)**: _void_  
The creature at attackerIndex of this army attacks the creature at targetIndex of the enemy army. If the target creature dies, the enemy's army size must be reduced by one and the dead creature removed from it.

<hr>

**AutoAttack(Commander enemy)**: _void_  
Find the best attacking creature from the attacking commander army and perform a damaging attack to its best target. Basically, for each creature you will have to use its `FindBestTarget` method and than for each 'best target', decide which one will be most damaged. Again, possible kills take priority.

Check `AutoAttack_Should.cs` in the **Commander** folder to get a better understanding.
using GameCreatures.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Creatures.Services.Contracts
{
    public interface IBestTargetFinder
    {
        Creature FindBestTarget(List<Creature> targets, Creature attacker);
    }
}

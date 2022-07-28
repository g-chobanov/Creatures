using System;
using System.Collections.Generic;
using System.Text;

namespace Creatures.Contracts
{
    public interface IDamagable
    {
        void TakeDamage(int damage);
    }
}

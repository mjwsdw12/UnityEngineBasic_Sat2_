using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInheritanceAndPolymorphism
{
    internal class EnemyTwoLeggedWalkable : Enemy, ITwoLeggedWalkable
    {
        public void TwoLeggedWalk()
        {
            Console.WriteLine($"Enemy {name} (이)가 두발로 걸었다");
        }
    }
}

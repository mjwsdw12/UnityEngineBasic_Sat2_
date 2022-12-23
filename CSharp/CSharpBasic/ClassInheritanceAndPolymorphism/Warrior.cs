using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInheritanceAndPolymorphism
{
    internal class Warrior : Player
    {
        public void Smash()
        {
            Console.WriteLine($"{nickName} (이)가 휘둘렀다");
        }
    }
}

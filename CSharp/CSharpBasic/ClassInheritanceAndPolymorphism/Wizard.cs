using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInheritanceAndPolymorphism
{
    internal class Wizard : Player
    {
        public void FireBall()
        {
            Console.WriteLine($"{nickName} (이)가 불덩이를 던졌다");
        }
    }
}

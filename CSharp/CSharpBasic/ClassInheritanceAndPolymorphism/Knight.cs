using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInheritanceAndPolymorphism
{
    internal class Knight :Player
    {

        public override void Attack()
        {
            // base : 부모객체 참조 키워드
            Console.WriteLine($"Knight {nickName} (이)가 공격했다");
        }
        public void Dash()
        {
            Console.WriteLine($"{nickName} (이)가 돌진했다.");
        }
    }
}

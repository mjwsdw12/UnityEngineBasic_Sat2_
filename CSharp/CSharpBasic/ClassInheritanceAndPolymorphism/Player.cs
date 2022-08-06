using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInheritanceAndPolymorphism
{
    internal class Player : Creature, ITwoLeggedWalkable
    {
        public string nickName;

        // virtual
        // 가상키워드, 해당 함수를 재정의 할 수 있도록 해줌
        // virtual함수가 호출되면 최하단에 override 된 함수를 호출
        public virtual void Attack()
        {
            Console.WriteLine($"{nickName} (이)가 공격했다!");
        }

        public void Jump()
        {
            Console.WriteLine($"{nickName} (이)가 점프했다!");
        }

        public void TwoLeggedWalk()
        {
            Console.WriteLine($"{nickName} (이)가 이족보행했다.");
        }

        public override void Grow()
        {
            throw new NotImplementedException();
        }
    }
}

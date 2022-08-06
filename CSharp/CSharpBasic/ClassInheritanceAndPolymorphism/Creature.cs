using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInheritanceAndPolymorphism
{
    // abstract 추상화 키워드
    internal abstract class Creature
    {
        public string DNA;
        public float mass;
        public void Breath()
        {
            Console.WriteLine($"{DNA} 형질을 가진 생명체가 숨을 쉰다");
        }

        public abstract void Grow();
    }
}

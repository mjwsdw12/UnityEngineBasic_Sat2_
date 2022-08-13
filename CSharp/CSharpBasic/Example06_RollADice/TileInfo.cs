using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example06_RollADice
{
    internal class TileInfo
    {
        public int index;
        public string name;
        public string description;
        public virtual void OnTile()
        {
            Console.WriteLine($"칸 번호 : {index}, 플레이어가 {name} 위에 도착했습니다. 이 칸은 {description}");
        }
    }
}

using System;

namespace ClassInheritanceAndPolymorphism
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creature creature = new Creature();
            Player player = new Player();
            // creature.Breath();
            player.Breath();

            Warrior warrior = new Warrior();
            Wizard wizard = new Wizard();
            Knight knight = new Knight();

            warrior.Smash();
            wizard.FireBall();
            knight.Dash();

            // 자식객체를 부모타입으로 참조할 수 있다.
            // 부모타ㅣ입으로 참조시 자식객체만의 멤버에는 접근할 수 없다.
            Player[] players = new Player[3];
            players[0] = warrior;
            players[1] = wizard;
            players[2] = knight;

            for (int i = 0; i < players.Length; i++)
            {
                players[i].Attack();
            }


            ITwoLeggedWalkable[] twoLeggedWalkables = new ITwoLeggedWalkable[3];
            twoLeggedWalkables[0] = new Warrior();
            twoLeggedWalkables[1] = new EnemyTwoLeggedWalkble();
            twoLeggedWalkables[2] = new Wizard();

            for (int i = 0; i < twoLeggedWalkables.Length; i++)
            {
                twoLeggedWalkables[i].TwoLeggedWalk();
            }

        }
    }
}

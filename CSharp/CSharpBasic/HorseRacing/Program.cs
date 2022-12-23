using System;
using System.Threading;
//프로그램 시작시
//말 다섯마리를 만들고
//각 다섯마리는 초당 10~20 (정수형) 범위 거리를 랜덤하게 움직임
//각각의 말이 거리 200 에 도달하면 말의 이름과 등수를 출력해줌
//
//말은
//이름, 달린거리 를 멤버변수로
//달리기 를 멤버 함수로 가짐.
//달리기 멤버함수는 입력받은 거리를 달린거리에 더해주어서 달린거리를 누적시키는 역할을 함.
//
//매초 달릴 때 마다 각 말들이 얼마나 거리를 이동했는지 콘솔창에 출력해줌.
//경주가 끝나면 1,2,3,4,5 등 말의 이름을 등수 순서대로 콘솔창에 출력해줌.

namespace Example02_HorseRacing
{
    internal class Program
    {
        static int totalHorses = 5;
        static Random random;
        static int minSpeed = 5;
        static int maxSpeed = 20;
        static bool isFinished = false;
        static int finishDistance = 100;
        static void Main(string[] args)
        {
            int grade = 0;
            int[] finishedHorsesIndex = new int[totalHorses];

            // for (5) { horse = new horse(); horse.name = 경주마 i }
            Horse[] horses = new Horse[totalHorses];
            for (int i = 0; i < horses.Length; i++)
            {
                horses[i] = new Horse();
                horses[i].name = $"경주마 {i + 1}";
                horses[i].enabled = true;
            }

            Console.WriteLine("경주 시작!");
            int count = 0;
            while (!isFinished)
            {
                Console.WriteLine($"========================{count} 초 경과 =====================");
                // for (5) {
                //  if 말이 달릴수 있으면
                //   랜덤속도 구함
                //   horse.Run(랜덤속도)
                //  if (horse 가 finishDistance 이상 달림) { 해당 말 더이상 못달리게 함, 등수 증가시킴 }
                // }
                // if (마지막 등수가 전체 말 수보다 크다면) { 게임 종료}

                for (int i = 0; i < horses.Length; i++)
                {
                    if (horses[i].enabled)
                    {
                        random = new Random();
                        int tmpMoveDistance = random.Next(minSpeed, maxSpeed + 1);
                        horses[i].Run(tmpMoveDistance);
                        Console.Write($"{horses[i].name} 이 달린 거리 : {horses[i].distance}");
                        if (horses[i].distance >= finishDistance)
                        {
                            horses[i].enabled = false;
                            finishedHorsesIndex[grade] = i;
                            grade++;
                            Console.Write($", {grade} 등으로 도착");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine($"{horses[i].name}는 도착함");
                    }
                }

                if (grade >= horses.Length)
                {
                    isFinished = true;
                    Console.WriteLine("경주 끝!");
                    break;
                }

                count++;
                Thread.Sleep(1000);
            }

            Console.WriteLine("착순표");
            for (int i = 0; i < finishedHorsesIndex.Length; i++)
            {
                Console.WriteLine($"{horses[finishedHorsesIndex[i]].name} 는 {i + 1} 등");
            }
        }
    }

    public class Horse
    {
        public string name;
        public int distance;
        public bool enabled;

        public void Run(int moveDistance)
        {
            distance += moveDistance;
        }
    }
}

using System;

// 맵 생성
//  while 주사위 갯수가 0이 아니라면
//{
// 현재 주사위 갯수
// 엔터키 입력 대기
// while (엔터 입력 들어올 때 까지)
//{
//         입력받음
//         if (입력이 엔터면)
//             break;
//         else
//               Console.WriteLine ("잘못된 입력입니다. 엔터키를 눌러주세요")
//}

// 주사위 굴림
// 주사위 눈금 = 1부터 6까지 랜덤한 숫자 생성

// 주사위 갯수 차감
// 주사위 눈금만큼 플레이어 전진
// 플레이어 위치 += 주사위 눈금

// 플레이어가 샛별칸 몇개 지났는지 체크
// 지난 샛별칸 갯수 = 플레이어위치 / 5 - 이전플레이어위치/ 5
// for (i = 0; i <지난 샛별칸 갯수만큼, i++)
//{
// 지난 샛별칸 인덱스 = (플레이어 위치 / 5 - i) * 5
// 지난 샛별칸 인덱스에 해당하는 TileInfo 받아오기
// 해당 샛별칸의 starValue 만큼 샛별 누적
//}


// if (플레이어 위치 > 전체 맵 타일 갯수)
//      플레이어위치 -= 전체 맵 타일 갯수

// 플레이어 위치에 해당하는 TileInfo Event 함수 호출
//}


//  주사위 갯수가 0이면
//{
// Console.WriteLine(게임끝, 총 획득 샛별수 : ?)
//}
namespace Example06_RollADice
{
    internal class Program
    {
        static private int totalDiceNumber = 20;
        static private int currentPos;
        static private int previousPos;
        static private int totalMapSize = 20;
        static private int totalStarPoint;
        static Random random;
        static void Main(string[] args)
        {
            // 맵 생성
            TileMap map = new TileMap();
            map.MapSetUp(totalMapSize);

            int currentDiceNumber = totalDiceNumber;
            while (currentDiceNumber > 0)
            {
                int diceValue = RollADice();
                currentDiceNumber--;

                // 플레이어 전진
                currentPos += diceValue;

                // 샛별 획득
                EarnStarValue(map, currentPos, previousPos);

                if (currentPos > totalMapSize)
                    currentPos -= totalMapSize;

                if (map.TryGetTileInfo(currentPos, out TileInfo tileInfo))
                {
                    tileInfo.OnTile();
                }
                else
                {
                    throw new Exception("플레이어가 맵을 이탈했습니다.");
                }

                previousPos = currentPos;
                Console.WriteLine($"현재 샛별점수 : {totalStarPoint}");
                Console.WriteLine($"남은 주사위 갯수 : {currentDiceNumber}");
            }

            Console.WriteLine($"게임 끝! 총 샛별 점수 : {totalStarPoint}");
        }

        static private int RollADice()
        {
            // 엔터 입력 대기
            string userInput = "Default";
            while (userInput != "")
            {
                Console.WriteLine("주사위를 굴리려면 엔터키를 누르세요");
                userInput = Console.ReadLine();
                if (userInput != "")
                    Console.WriteLine("아니 이거말고 엔터키 누르라고");
                else
                    break;
            }

            // 주사위 굴림
            random = new Random();
            int diceValue = random.Next(1, 7);
            DisplayDice(diceValue);
            return diceValue;
        }

        static private void EarnStarValue(TileMap map, int currentPos, int previousPos)
        {
            // 플레이어가 샛별칸 몇개 지났는지 체크
            int passedStarTileNum = currentPos / 5 - previousPos / 5;
            for (int i = 0; i < passedStarTileNum; i++)
            {
                int starTileIndex = (currentPos / 5 - i) * 5;

                if (starTileIndex > totalMapSize)
                    starTileIndex = totalMapSize;

                if (map.TryGetTileInfo(starTileIndex, out TileInfo tileInfo_star))
                {
                    totalStarPoint += (tileInfo_star as TileInfo_Star).starValue;
                }
                else
                {
                    throw new Exception("샛별 칸 정보를 가져오는데 실패했습니다");
                }
            }
        }

        static private void DisplayDice(int diceValue)
        {
            Console.WriteLine($"주사위 눈금은 {diceValue}가 나왔다네~");
            switch (diceValue)
            {
                case 1:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│     ●    │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("└───────────┘");
                    break;
                case 2:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●        │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│         ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                case 3:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●        │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│     ●    │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│         ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                case 4:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                case 5:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│     ●    │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                case 6:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                default:
                    throw new Exception("주사위 눈금이 잘못되었어여");
                    break;

            }
        }

    }
}

using System;
/*
엔터키 입력으로 주사위를 굴립니다.
주사위를 굴리면 플레이어가 전진하고, 샛별칸에 도착하거나 지나갈 시 샛별에 대한 이벤트가 발생합니다.
총 칸은 1에서 20까지 있으며, 20을 넘어가면 다시 1부터 전진을 계속합니다.
5배수 칸은 샛별칸이고, 이 칸을 지나거나 도착하면 샛별을 획득할 수 있습니다.
5배수 칸에 도착할 때에는 샛별 획득 개수가 영구적으로 1 증가합니다.
샛별을 획득할 시 , 새로 얻은 샛별 수와 총 획득한 샛별 수를 보여줍니다.
콘솔 출력 :
주사위를 돌려서 어떤 칸에 도착하면,
해당 칸의 번호 (1~20 중 하나 ), 해당 칸이 어떤칸인지 (그냥 일반인지 샛별인지 ),
현재 샛별수는 몇개인지 , 남은 주사위 수는 몇개인지 콘솔창에 출력해주고
다시 주사위를 굴리라고 콘솔창에 출력해줌.
주사위를 다쓰면 모은 샛별 수를 출력해주고 게임을 종료함. (초기 주사위 갯수 20개)
### - Hint
만들어야 하는 클래스 :
TileMap(맵을 세팅하고 맵에대한 정보를 가지고 있을 클래스)
TileInfo(각 칸들의 정보를 멤버로 가지는 클래스)
TileInfo_Star(샛별칸을 위한 클래스.TileInfo 를 상속받고 샛별칸에 대한 특수 정보를 멤버로 추가함)
주사위는 아래처럼 콘솔창에 찍어서 보여주면 됨.
Console.WriteLine("┌───────────┐");
Console.WriteLine("│ ●      ●│");
Console.WriteLine("│           │");
Console.WriteLine("│     ●    │");
Console.WriteLine("│           │");
Console.WriteLine("│ ●      ●│");
Console.WriteLine("└───────────┘");
*/

/*
주사위게임 로직 
의사코드 작성해보기
문법 같은거 전혀 상관엄씀 . 본인만 알아보믄댐

의사코드힌트

while( 주사위 갯수 남아있을 떄 까지)
{
엔터키 입력 대기
while (엔터 입력 들어올떄까지)
{
	입력받음
	if ( 입력이 엔터면)
		break;
	else
		Console.wriline(잘못된 입력입니다. 엔터키를 누르세요)
}

주사위눈금 = 1부터 6까지 랜덤한 숫자 생성
주사위 갯수 차감
//주사위 눈금만큼 플레이어 전진
플레이어위치 += 주사위눈금

//플레이어가 샛별칸 몇개 지났는지 체크
지난 샛별칸 갯수 = 플레이어위치 /5 - 이전플레이어위치/5
for (i = 0; i < 지난 샛별칸 갯수만큼; i++)
{
 지난샛별칸 인덱스 = (플레이어위치 / 5 - i) * 5	
 지난샛별칸 인덱스에 해당하는 TileInfo 받아오기 
 해당 샛별칸의 starValue 만큼 샛별점수 누적
}

if (플레이어위치 > 전체 맵 타일 갯수)
	플레이어위치 -= 전체 맵 타일 갯수

플레이어 위치에 해당하는 TileInfo Event 함수 호출
}

Console.writeline(게임끝, 총 획득샛별수 : ?)
*/
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
                    throw new Exception("플레이어가 맵을 이탈했습니다");
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
                    starTileIndex -= totalMapSize;

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
            Console.WriteLine($"주사위 눈금은 {diceValue} 가 나왔다네~");
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
            }
        }

    }
}

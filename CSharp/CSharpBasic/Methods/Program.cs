using System;

// Method ⊂ Function
// 객체/ 클래스를 통해서 호출하는 함수를 Method 라고함

namespace Methods
{
    internal class Program
    {
        // 멤버 함수
        // 클래스 내에 정의된 함수
        static void Main(string[] args)
        {
            // 함수 호출
            // 함수이름(인자, 인자 ...);
            // 인자 : 매개변수에 넣어줄 값
            PrintHelloWorld();
            PrintString("안녕");
            int num = PrintStringWithReturnLength("안녕못해");
            Console.WriteLine(num);
        }

        // 함수 정의
        // 접근제한자 /static/ 반환형 함수이름 (매개변수자료형 매개변수이름, 매개변수자료형 매개변수이름)
        // {
        //      return 반환값;
        // }
        static void PrintHelloWorld()
        {
            Console.WriteLine("Hello World!");
            return;
        }

        static void PrintString(string target)
        {
            Console.WriteLine(target);
        }

        // 멤버함수 
        // 클래스 내에 정의된 함수
        static int PrintStringWithReturnLength(string target)
        {
            // 지역변수
            int tmpLength = target.Length;
            Console.WriteLine(target);
            return tmpLength;
        }
    }
}

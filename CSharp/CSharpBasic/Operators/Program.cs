using System;

namespace Operators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 14;
            int b = 6;
            int c = 0;

            // 산술 연산자
            // 더하기, 빼기, 곱하기, 나누기, 나머지
            //============================================================

            // 더하기
            c = a + b;
            Console.WriteLine(c);

            // 빼기
            c = a - b;
            Console.WriteLine(c);

            // 곱하기
            c = a * b;
            Console.WriteLine(c);

            // 나누기
            c = a / b; // 정수 나눗셈은 소숫점을 버린 정수를 반환
            Console.WriteLine(c);

            // 나머지
            c = a % b; // 자료형 관계없이 정수 나눗셈 후 나머지 결과를 반환
            Console.WriteLine(c);

            // 증감 연산자
            // 증가 연산자, 감소 연산자
            //======================================================

            // 증가 연산
            ++c; // 전위연산 : 해당 라인의 연산을 먼저 수행한 뒤 명령 실행 
            c++; // 후위연산 : 해당 라인에서 명령을 실행한 뒤 연산 수행
            c = 0;
            Console.WriteLine("증가연산 출력");
            Console.WriteLine(c);
            Console.WriteLine(++c);
            Console.WriteLine(c++);
            Console.WriteLine(c);

            // 감소 연산
            --c;
            c--;

            // 관계 연산자
            // 같음, 다름, 크기 등의 비교 연산자
            // 관계 연산자의 연산결과가 참이면 true, 거짓이면 false 반환
            //==============================================================

            Console.WriteLine("관계연산 출력");

            // 같음 비교
            Console.WriteLine(a == b);

            // 다름 비교
            Console.WriteLine(a != b);

            // 크기 비교
            Console.WriteLine(a > b);
            Console.WriteLine(a >= b);
            Console.WriteLine(a < b);
            Console.WriteLine(a <= b);

            // 대입 연산자
            //====================================================
            c = 20;
            c += b; // == c = c + b;
            c -= b;
            c *= b;
            c /= b;
            c %= b;

            // 논리 연산자
            // or, and, xor, not
            //======================================================

            bool A = true;
            bool B = true;
            Console.WriteLine("논리 연산 출력");

            // or
            // 둘 중 하나라도 참이면 true 반환
            Console.WriteLine(A | B);

            // and
            // 둘 다 참일때만 true 반환
            Console.WriteLine(A & B);

            // xor
            // 둘 중 하나만 참일때 true 반환
            Console.WriteLine(A ^ B);

            // not 
            // 피연산자가 true 이면 false, false 이면 true 반환
            Console.WriteLine(!A);

            // 조건부 논리연산자
            // 조건부 or, 조건부 and
            //===================================================
            
            // 조건부 or
            // 첫번째 피연산자가 true면 B비교 하지 않고 true반환
            Console.WriteLine(A || B);

            // 조건부 and
            // 첫번째 피연산자가 false 면 B비교 하지 않고 false 반환
            Console.WriteLine(A && B);

            //if (swordMan != null &&
            //    swordMan.Lv > 0)
            //{
            //    Console.WriteLine(swordMan.Lv);
            //}

            // 비트 연산자
            // or, and, xor, not, shift-left, shift-right
            // 정수형 연산할때 보통 씀
            //========================================================
            int howManyBitYouWantToShift = 2;

            Console.WriteLine("비트연산 출력");
            Console.WriteLine(a | b);
            Console.WriteLine(a & b);
            Console.WriteLine(a ^ b);
            Console.WriteLine(~a);
            Console.WriteLine(a << howManyBitYouWantToShift);
            Console.WriteLine(a >> howManyBitYouWantToShift);
        }
    }
}

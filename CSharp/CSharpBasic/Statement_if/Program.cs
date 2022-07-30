using System;

namespace Statement_if
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool condition1 = true;
            bool condition2 = false;
            bool condition3 = false;

            //if (조건)
            //{
            //    조건이 참일때 실행할 내용
            //}
            if (condition1)
            {
                Console.WriteLine("조건 1이 참");
            }
            else if (condition2)
            {
                Console.WriteLine("조건 1이 거짓, 조건 2가 참");
            }
            else if (condition3)
            {
                Console.WriteLine("조건 1이 거짓, 조건 2가 거짓, 조건 3이 참");
            }
            else
            {
                Console.WriteLine("조건 1이 거짓, 조건 2가 거짓, 조건 3이 거짓");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    public static class OPs
    {
        public static int opCount;

        public static void Init()
        {
            Program.RegisterCallBack(Sum);
            Program.RegisterCallBack(Sub);
            Program.opDelegate += Mul;
            // Program.opDelegate(1, 2) // event 한정자때문에 호출 할 수 없음
        }

        public static int Sum(int a, int b)
        {
            Console.WriteLine($"OP : Sum(), result : {a + b}");
            opCount++;
            return a + b;
        }
        public static int Sub(int a, int b)
        {
            Console.WriteLine($"OP : Sub(), result : {a - b}");
            opCount++;
            return a + b;
        }
        public static int Mul(int a, int b)
        {
            Console.WriteLine($"OP : Mul(), result : {a * b}");
            opCount++;
            return a + b;
        }
        public static int Div(int a, int b)
        {
            Console.WriteLine($"OP : Div(), result : {a / b}");
            opCount++;
            return a + b;
        }
        public static int Mod(int a, int b)
        {
            Console.WriteLine($"OP : Mod(), result : {a % b}");
            opCount++;
            return a + b;
        }
    }
}

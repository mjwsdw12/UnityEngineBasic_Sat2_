using System;

namespace Array
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // array
            // 형태 : 자료형[] / * []는 인덱서라고 함
            // 자료형이 정적으로, 연속적을 나열되어 있는 형태
            // 한번크기를 정하면 일반적으로는 바꿀 수 없다.

            int[] arrInt = new int[3];
            int[] arrInt2 = {1, 2, 3};
            float[] arrFloat = new float[4];

            arrInt[0] = 3; // arrInt 의 첫번째 주소 + 0 * 자료형 크기 의 주소부터 자료형크기 만큼 접근한다.
            arrInt[1] = 4; // arrInt 의 첫번째 주소 + 1 * 자료형 크기 의 주소부터 자료형크기 만큼 접근한다.
            Console.WriteLine(arrInt2[2]);

            Console.WriteLine($"arrInt2 length : { arrInt2.Length}");
        }
    }
}

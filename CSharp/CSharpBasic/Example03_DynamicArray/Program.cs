using System;
using System.Collections.Generic;
namespace Example03_DynamicArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DynamicArray<int> dynamicArray = new DynamicArray<int>();
            dynamicArray.Add(1);
            Console.WriteLine(dynamicArray.Capacity);
            dynamicArray.Add(2);
            Console.WriteLine(dynamicArray.Capacity);
            for (int i = 0; i < 50; i++)
            {
                dynamicArray.Add(2);
            }
           
            Console.WriteLine(dynamicArray.Capacity);
            Console.WriteLine(dynamicArray.Count);
            dynamicArray.RemoveAt(0);
            Console.WriteLine(dynamicArray.Count);
            for (int i = 0; i < dynamicArray.Count; i++)
            {
                Console.WriteLine(dynamicArray[i]);
            }
            foreach (int item in dynamicArray)
            {
                Console.WriteLine(item);
            }

            // IDisposeble 인터페이스를 상속받은 객체의 Dispose 호출을 보장받는 using 구문
            using (IEnumerator<int> enumerator = dynamicArray.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Console.WriteLine(enumerator.Current);
                }
                enumerator.Reset();
            }
        }
    }
}

using System;

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
            for(int i = 0; i < 50; i++)
            {
                dynamicArray.Add(2);
            }
            Console.WriteLine(dynamicArray.Capacity);
            Console.WriteLine(dynamicArray.Count);
            dynamicArray.RemoveAt(0);
            Console.WriteLine(dynamicArray.Count);
        }
    }
}

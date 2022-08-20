using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // System.Collections
            //----------------------------------------------------

            // 박싱 : 기존 자료형을 객체로 변환하는 과정
            // 언박싱 : 객체를 특정 자료형으로 변환하는 과정
            ArrayList arrayList = new ArrayList();
            arrayList.Add ((object)1);  // 명사적 형변환
            arrayList.Add("dfdf");      // 암시적 형변환
            arrayList.Add('o');
            arrayList.Remove('o');
            arrayList.RemoveAt(0);
            for (int i = 0; i < arrayList.Count; i++)
            {
                Console.WriteLine(arrayList[i]);
            }

            Hashtable hashTable = new Hashtable();
            hashTable.Add("철수", 90);
            Console.WriteLine(hashTable["철수"]);

            // System.Collections.Generic
            // Generic : 일반화 (타입이 정해져있지 않은 경우에 대해 정의해놓은 타입)
            //----------------------------------------------------
            // 박싱, 언박싱 X

            // List
            List<int> list = new List<int>();
            List<string> strinList = new List<string>();
            list.Add(1);
            list.Add(2);
            list.Remove(2);
            list.RemoveAt(0);
            list.Find(x => x == 1);
            list.FindIndex(x => x == 1);
            // Console.WriteLine(list[0]);
            for (int i = 0; i <list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
            foreach (int item in list)
            {
                Console.WriteLine(item);
            }

            // Linked List
            // c# 에서 LinkedList 는 doubly-linkedList
            LinkedList<int> linkedList = new LinkedList<int>();
            linkedList.AddLast(1);
            linkedList.AddFirst(2);

            // Dictionary
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("사과", "쌍떡잎식물 장미목 장미과 낙엽교목 식물인 사과나무의 열매로, 이과에 속하며 지름 5~10cm 정도의 둥근 모양으로 빚깔은 보통 붉거나 노랗다.");
            Console.WriteLine(dictionary["사과"]);

            // Queue
            // FIFO (First-Input, First-Output system)
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("철수");
            queue.Enqueue("영희");
            queue.Enqueue("지희");
            Console.WriteLine(queue.Peek());
            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(queue.Peek());

            // Stack
            // LIFO (Last-Input, First-Output system)
            Stack<string> stack = new Stack<string>();
            stack.Push("철수");
            stack.Push("영희");
            stack.Push("지희");
            Console.WriteLine(stack.Peek());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Peek()); 
        }
    }
}

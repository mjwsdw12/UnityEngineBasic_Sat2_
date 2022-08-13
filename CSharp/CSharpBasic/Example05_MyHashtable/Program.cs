using System;

namespace Example05_MyHashtable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyHashtable<string, int> myHashtable = new MyHashtable<string, int>();
            myHashtable.Add("철수", 90);
            myHashtable.Add("영희", 80);
            myHashtable.Remove("영희");
        }
    }
}

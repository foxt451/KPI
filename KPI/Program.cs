using System;

namespace KPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Test test = new TestParser().ReadTestFromFile("test1.txt");
            Console.WriteLine("Hello World!");
        }
    }
}

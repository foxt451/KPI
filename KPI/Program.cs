using System;

namespace KPI
{
    class Program
    {
        static void Main(string[] args)
        {
            
                new TestOutputter().RunTest(new TestParser().ReadTestFromFile("test1"));
        }
    }
}

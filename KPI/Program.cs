using System;

namespace KPI
{
    class Program
    {
        static void Main(string[] args)
        {
            LectureDataBase db = new LectureDataBase();
            Output outputter = new Output(db);
            outputter.StartWork();
        }
    }
}

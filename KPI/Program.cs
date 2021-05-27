using System;
using System.Collections.Generic;

namespace KPI
{
    class Program
    {
        static void Main(string[] args)
        {
            LectureDataBase db = new LectureDataBase();
            Dictionary glossary = new Dictionary();
            Output outputter = new Output(db, glossary);
            outputter.StartWork();
        }
    }
}

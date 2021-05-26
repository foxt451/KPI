using System;
using System.Collections.Generic;
using System.Linq;

namespace KPI
{
    public class LectureCreator
    {
        public void CreateLectureUsingConsole(LectureDataBase db)
        {
            Console.Clear();
            Console.WriteLine("Lecture creation menu");
            Console.Write("Enter lecture title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Enter the lecture material line by line ('q' - to complete input)");
            Console.WriteLine("Enter text of lecture:");
            bool inputting = true;
            List<string> blocksOfLecture = new List<string>();
            string blockOfLecture = "";
            while (inputting)
            {
                string str = Console.ReadLine();
                if (String.Compare(str, "q") == 0)
                    inputting = false;
                else
                {
                    if (str.Count(ch => ch == '-') >= 50)
                    {
                        if (blockOfLecture.Length > 0)
                            blocksOfLecture.Add(blockOfLecture);
                        blockOfLecture = "";
                    }
                    else
                        blockOfLecture = String.Concat(blockOfLecture, str);
                }
            }
            if (blockOfLecture.Length > 0)
                blocksOfLecture.Add(blockOfLecture);
            db.AddNewLecture(title, blocksOfLecture);
        }
    }
}
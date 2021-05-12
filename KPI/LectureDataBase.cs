using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;

namespace KPI
{
    public class LectureDataBase
    {
        public List<Lecture> lectures;
        public LectureDataBase()
        {
            this.lectures = this.getLectures();
        }
        public List<Lecture> getLectures()
        {
            List<Lecture> lectures = new List<Lecture>();
            string[] files = Directory.GetFiles("./lectures");
            for (int i = 0; i < files.Length; i++)
            {
                string name;
                List<string> blocksOfLecture = new List<string>();
                using (StreamReader sr = new StreamReader(files[i]))
                {
                    name = sr.ReadLine();
                    string blockOfLecture = "";
                    while (!sr.EndOfStream)
                    {
                        string str = sr.ReadLine();
                        if (str.Count(ch => ch == '-') >= 50)
                        {
                            if (blockOfLecture.Length > 0)
                                blocksOfLecture.Add(blockOfLecture);
                            blockOfLecture = "";
                        }
                        else
                            blockOfLecture = String.Concat(blockOfLecture, str, '\n');
                    }

                    if (blockOfLecture.Length > 0)
                    {
                        blocksOfLecture.Add((blockOfLecture));
                        blockOfLecture = "";
                    }
                }
                Lecture lecture = new Lecture(i + 1, name, blocksOfLecture, null);
                lectures.Add(lecture);
            }

            return lectures;
        }
    }
}
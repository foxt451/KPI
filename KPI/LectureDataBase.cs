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
            this.lectures = this.GetLectures();
        }
        public List<Lecture> GetLectures()
        {
            TestParser testParser = new TestParser();
            List<Lecture> lectures = new List<Lecture>();
            string[] systemFiles = Directory.GetFiles("../../../Lectures/");
            List<string> files = new List<string>();
            foreach (var systemFile in systemFiles)
            {
                int index = systemFile.LastIndexOf('.');
                string part = systemFile.Substring(index, systemFile.Length - index);
                if (String.Compare(part, ".txt") == 0)
                {
                    files.Add(systemFile);
                }
            }
            for (int i = 0; i < files.Count; i++)
            {
                string title;
                Test test = testParser.ReadTestFromFile($"test_{i + 1}");
                List<string> blocksOfLecture = new List<string>();
                using (StreamReader sr = new StreamReader(files[i]))
                {
                    title = sr.ReadLine();
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
                Lecture lecture = new Lecture(i + 1, title, blocksOfLecture, test);
                lectures.Add(lecture);
            }

            return lectures;
        }

        public void AddNewLecture(string title, List<string> blocksOfLecture)
        {
            int number = this.lectures.Count;
            Lecture lecture = new Lecture(number, title, blocksOfLecture, null);
            this.lectures.Add(lecture);
            FileOperations fstream = new FileOperations("../../../Lectures", $"/lec_{number + 1}.txt");
            fstream.AddToFile(title);
            foreach (string block in blocksOfLecture)
            {
                fstream.AddToFile(block);
                string separator = "";
                for (int i = 0; i < 75; i++)
                    separator += "-";
                fstream.AddToFile(separator);
            }
        }
    }
}
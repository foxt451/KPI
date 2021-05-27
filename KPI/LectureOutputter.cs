using System;
using System.Linq;

namespace KPI
{
    public class LectureOutputter
    {
        public void OutputLecture(Lecture lecture)
        {
            int numberOfLectureBlock = 0;
            bool needToOutputLecture = true;
            if (lecture.lectureBlocks.Count == 0)
                numberOfLectureBlock = -1;
            while (needToOutputLecture)
            {
                Console.Clear();
                Console.WriteLine(lecture.title);
                if (numberOfLectureBlock != -1)
                {
                    Console.WriteLine(lecture.lectureBlocks[numberOfLectureBlock]);
                    if (numberOfLectureBlock > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("p - to go the previous part of lecture");
                    }
                    if (numberOfLectureBlock < lecture.lectureBlocks.Count - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("n - to go to the next part of lecture");
                    }
                    else
                    {
                        Console.WriteLine("t - go to the associated test");
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("q - to quit lecture");
                Console.ResetColor();
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                if (choice[0] == 'q')
                    needToOutputLecture = false;
                else if (numberOfLectureBlock != -1)
                {
                    if (choice[0] == 'n')
                    {
                        if (numberOfLectureBlock < lecture.lectureBlocks.Count - 1)
                            numberOfLectureBlock++;
                    }
                    if (choice[0] == 'p')
                    {
                        if (numberOfLectureBlock > 0)
                            numberOfLectureBlock--;
                    }
                    if (choice[0] == 't')
                    {
                        if (lecture.test != null)
                        {
                            new TestOutputter().RunTest(lecture.test);
                        }
                        else
                        {
                            Console.WriteLine("No test for this lecture!");
                        }
                    }
                }
            }
        }
    }
}
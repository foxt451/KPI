using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI
{
    class TestCreator
    {

        public (bool interrupted, Test test) CreateTestInConsole()
        {
            Test test = new();
            var title = ReadTitle();
            if (title.interrupted)
            {
                return (true, test);
            }
            else
            {
                test.title = title.title;
            }

            while(true)
            {
                Console.WriteLine("Add a question? (y/n):");
                if (Console.ReadLine()[0] == 'y')
                {
                    var question = ReadQuestion();
                    test.questions.Add(question);
                }
                else
                {
                    break;
                }
            }

            return (false, test);
        }

        private TestQuestion ReadQuestion()
        {
            TestQuestion question = new();

            Console.WriteLine("Enter question text (enter 'q' when over):");
            string text = GetUntilKey("q");
            question.question = text;

            Console.WriteLine("Enter number of options:");
            int optionsNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the order of the correct option (starting from 1):");
            int correctIndex = int.Parse(Console.ReadLine());
            question.answerIndex = correctIndex - 1;

            List<string> options = new();
            for (int i = 0; i < optionsNumber; i++)
            {
                Console.WriteLine($"Enter option {i + 1} (enter q when over):");
                string option = GetUntilKey("q");
                options.Add(option);
            }

            question.options = options;

            return question;
        }

        private string GetUntilKey(string key)
        {
            string text = "";
            while (true)
            {
                string line = Console.ReadLine();
                if (line == key)
                {
                    break;
                }
                else
                {
                    text += line + "\n";
                }
            }
            return text;
        }

        private (bool interrupted, string title) ReadTitle()
        {
            Console.WriteLine("Enter lecture number:");
            string line = Console.ReadLine();
            if (line == "q")
            {
                return (true, "");
            } else
            {
                try
                {
                    return (false, "test_" + int.Parse(line));
                }
                catch
                {
                    return (true, "");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPI
{
    class TestOutputter
    {
        private void OutputQuestion(TestQuestion question)
        {
            Console.WriteLine(question.question);
            for (int i = 0; i < question.options.Count; i++)
            {
                Console.WriteLine($"{i + 1})");
                Console.WriteLine(question.options[i]);
            }
        }

        private (bool quit, int answer) GetAnswer()
        {
            Console.WriteLine("Enter your answer or q to finish test:");
            while (true)
            {
                string answer = Console.ReadLine();
                if (answer[0] == 'q')
                {
                    return (true, -1);
                }
                else
                {
                    if (int.TryParse(answer, out int parsed))
                    {
                        return (false, parsed);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input, try again:");
                    }
                }
            }
        }

        public (bool wasInterrupted, int score) RunTest(Test test)
        {
            int score = 0;
            for (int i = 0; i < test.questions.Count; i++)
            {
                Console.WriteLine($"{i + 1}.");
                OutputQuestion(test.questions[i]);

                var answer = GetAnswer();
                if (answer.quit)
                {
                    return (true, score);
                }
                else
                {
                    if (test.questions[i].ValidateAnswer(answer.answer))
                    {
                        Console.WriteLine("Correct!");
                        score++;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect!");
                    }
                }
            }
            return (false, score);
        }
    }
}

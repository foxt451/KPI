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
            Console.Write(question.question);
            for (int i = 0; i < question.options.Count; i++)
            {
                Console.Write($"{i + 1}) ");
                Console.Write(question.options[i]);
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

        private void OutputAnswerReaction(bool wasCorrect, ConsoleColor defaultColor)
        {
            if (wasCorrect)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect!");
            }

            Console.ForegroundColor = defaultColor;
        }

        public (bool wasInterrupted, int score) RunTest(Test test)
        {
            Console.WriteLine(test.title);

            int score = 0;
            for (int i = 0; i < test.questions.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                OutputQuestion(test.questions[i]);

                var answer = GetAnswer();
                if (answer.quit)
                {
                    return (true, score);
                }
                else
                {
                    bool wasCorrect = test.questions[i].ValidateAnswer(answer.answer - 1);
                    if (wasCorrect)
                    {
                        score++;
                    }
                    Console.Clear();
                    OutputAnswerReaction(wasCorrect, ConsoleColor.White);

                    Console.WriteLine();
                }
            }
            return (false, score);
        }
    }
}
